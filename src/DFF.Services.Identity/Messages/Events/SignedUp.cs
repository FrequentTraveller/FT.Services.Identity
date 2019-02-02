using DFF.Common.Messages;
using Newtonsoft.Json;
using System;

namespace DFF.Services.Identity.Messages.Events
{
    public class SignedUp : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        [JsonConstructor]
        public SignedUp(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
