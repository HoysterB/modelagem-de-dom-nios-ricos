﻿using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects;

public abstract class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}