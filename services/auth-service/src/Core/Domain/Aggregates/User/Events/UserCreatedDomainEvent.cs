using System;
using Domain.Aggregates.User.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.User.Events
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }
        
        public UserCreatedDomainEvent(UserId userId)
        {
            UserId = userId;
        }
    }
}