﻿using FT.Common.Authentication;
using FT.Common.RabbitMq;
using FT.Common.Types;
using FT.Services.Identity.Domain;
using FT.Services.Identity.Messages.Events;
using FT.Services.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FT.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IBusPublisher _busPublisher;

        public IdentityService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IRefreshTokenRepository refreshTokenRepository,
            IBusPublisher busPublisher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _busPublisher = busPublisher;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string role = Role.User)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new FTException(Codes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }
            user = new User(id, email, role);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.CreateAsync(user);
            await _busPublisher.PublishAsync(new SignedUp(id, email), CorrelationContext.Empty);
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new FTException(Codes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new FTException(Codes.UserNotFound,
                    $"User: '{userId}' was not found.");
            }
            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new FTException(Codes.InvalidCurrentPassword,
                    "Invalid current password.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);
        }
    }
}
