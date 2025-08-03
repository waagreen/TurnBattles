using System;
using System.Collections.Generic;

public static class EventsManager
{
    private static readonly Dictionary<Type, Action<GameEvent>> activeEvents;
    private static readonly Dictionary<Delegate, Action<GameEvent>> actionsLookup;

    public static void AddSubscriber<T>(Action<T> evt) where T : GameEvent
    {
        if (actionsLookup.ContainsKey(evt)) return;

        // Local function that converts generic to the expected type <T>
        void action(GameEvent e) => evt((T)e);

        // Storing original delegate
        actionsLookup[evt] = action;

        if (activeEvents.TryGetValue(typeof(T), out var existingAction))
        {
            // If an event of that type already exists, subscribe the action to it
            activeEvents[typeof(T)] = existingAction += action;
        }
        else
        {
            // Otherwise, this will be the first instance of that event type
            activeEvents[typeof(T)] = action;
        }
    }

    public static void RemoveSubscriber<T>(Action<T> evt) where T : GameEvent
    {
        if (actionsLookup.TryGetValue(evt, out var action))
        {
            if (activeEvents.TryGetValue(typeof(T), out var existingAction))
            {
                existingAction -= action;

                if (existingAction == null)
                {
                    // If it was the last action subscribed to that event, remove that type from the dictionary
                    activeEvents.Remove(typeof(T));
                }
                else
                {
                    // Otherwise, there are actions still subscribed, so update the dictionary
                    activeEvents[typeof(T)] = existingAction;
                }
            }

            // Remove action from the lookup table
            actionsLookup.Remove(evt);
        }
    }

    public static void Broadcast(GameEvent evt)
    {
        if (activeEvents.TryGetValue(evt.GetType(), out var action))
        {
            action.Invoke(evt);
        }
    }

    public static void ClearAll()
    {
        activeEvents.Clear();
        actionsLookup.Clear();
    }
}
