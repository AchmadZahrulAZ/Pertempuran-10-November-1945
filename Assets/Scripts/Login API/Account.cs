using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Account
{
    public string id_player;
    public string username;
    public string nama_player;

    public EventLog[] eventLogs;
    public Dictionary<GameEvent, EventLog> eventLogDict = new Dictionary<GameEvent, EventLog>();

    public Account(string id, string username, string nama_player)
    {
        this.id_player = id;
        this.username = username;
        this.nama_player = nama_player;

        // //dummy (id_game 3 = Pertempuran 10 November 1945)
        // eventLogs = new EventLog[3];
        // eventLogs[0] = new EventLog(1, 1, EventStatus.selesai);
        // eventLogs[1] = new EventLog(1, 2, EventStatus.selesai);
        // eventLogs[2] = new EventLog(1, 3, EventStatus.belum);
        //end dummy

        // foreach(EventLog log in eventLogs)
        // {
        //     eventLogDict.Add(new GameEvent(log.id_game, log.no_event), log);
        // }
    }


    public void SetEventLog(List<List<EventLog>> eventLogs)
    {
        if(eventLogs == null)
        {
            return;
        }

        foreach(List<EventLog> logs in eventLogs)
        {
            foreach(EventLog log in logs)
            {
                if(log.id_game == APIManager.ID_GAME)
                {
                    continue;
                }
                eventLogDict[new GameEvent(log.id_game, log.no_event)] = log;
            }
        }
    }

    public bool CheckEventLog(GameEvent gameEvent, EventStatus status)
    {
        if(eventLogDict.ContainsKey(gameEvent))
        {
            return eventLogDict[gameEvent].status == status;
        }
        else
        {
            return false;
        }
    }
}


/// <summary>
/// Struct to simplify the Account's eventLog dictionary key
/// </summary>
public struct GameEvent
{
    public int id_game;
    public int no_event;

    public GameEvent(int id_game, int no_event)
    {
        this.id_game = id_game;
        this.no_event = no_event;
    }
}

/// <summary>
/// Event log based on database API.
/// Should be used to GET and POST event log data
/// </summary>
[System.Serializable]
public class EventLog
{
    public int id_game;
    public int id_log;
    public int no_event;
    [Newtonsoft.Json.JsonProperty("status_event")] public EventStatus status;


    /// <summary>
    /// Constructor for GET request
    /// </summary>
    // <param name="no_event"></param>
    // <param name="status"></param>
    public EventLog(int id_game, int no_event, EventStatus status)
    {
        this.id_game = id_game;
        this.no_event = no_event;
        this.status = status;
    }
}

/// <summary>
/// Status of an event based on database API.
/// Udah sesuai tinggal dikasih eventStatus.ToString() kalo di POST
/// </summary>
[System.Serializable]
public enum EventStatus
{
    belum,
    sedang,
    selesai
}