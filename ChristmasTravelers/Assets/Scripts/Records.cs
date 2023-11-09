using BoardCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Records
{

    /// <summary>
    /// Holds all of the necessary data to record
    /// </summary>
    [CreateAssetMenu(fileName = "RecordData", menuName = "Scriptables/RecordData", order = 0)]
    public class RecordData : ScriptableObject
    {
        public double recordFrequency;
    }


    /// <summary>
    /// Object from which we can records commands
    /// </summary>
    /// <typeparam name="BoardCommandType"></typeparam>
    public interface IRecordable<BoardCommandType> where BoardCommandType : IBoardCommand
    {
        /// <summary>
        /// Called when a command is requested
        /// </summary>
        public event Action<BoardCommandType> OnCommandRequest;
    }


    /// <summary>
    /// Object that can listen to a recordable, execute and store all of its commands
    /// </summary>
    /// <typeparam name="BoardCommandType"></typeparam>
    public interface IRecorder<BoardCommandType> where BoardCommandType : IBoardCommand
    {
        void BeginRecord();
        void EndRecord();
        /// <summary>
        /// Saves the current record in a replay
        /// </summary>
        /// <param name="replay"></param>
        void SaveRecord(Replay replay);
    }
}
