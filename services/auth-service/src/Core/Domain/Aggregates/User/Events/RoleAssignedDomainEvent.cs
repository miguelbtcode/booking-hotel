using Domain.Aggregates.Role.ValueObjects;
using Domain.Aggregates.User.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.User.Events
{
    public class RoleAssignedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }
        public RoleId RoleId { get; }
        
        public RoleAssignedDomainEvent(UserId userId, RoleId roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}