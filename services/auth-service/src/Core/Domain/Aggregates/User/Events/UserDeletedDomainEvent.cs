using System;
using Domain.Aggregates.User.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.User.Events
{
    public class UserDeletedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }
        
        public UserDeletedDomainEvent(UserId userId)
        {
            UserId = userId;
        }
    }
}