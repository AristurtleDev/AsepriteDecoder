/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

using System.Runtime.Serialization;

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an exception that occurs when the value of content within
    ///     an Aseprite file is invalid.
    /// </summary>
    [Serializable]
    public class InvalidContentException<T> : Exception, ISerializable
    {
        /// <summary>
        ///     Gets or Sets a string that contains the name of the value that
        ///     is invalid.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Gets or Sets a value that represents the the value that was
        ///     read but is invalid.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        ///     Creates a new <see cref="InvalidContentException"/> class
        ///     instance.
        /// </summary>
        /// <param name="message">
        ///     A message that describes the reason the exception occurred.
        /// </param>
        /// <param name="name">
        ///     The name of the value that was invalid.
        /// </param>
        /// <param name="value">
        ///     The value that was read but was invalid.
        /// </param>
        public InvalidContentException(string message, string name, T value)
         : base(message) => (Name, Value) = (name, value);

        /// <summary>
        ///     Special constructor used for deserialization.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo"/> class instance containing
        ///     the values needed for deserialization.
        /// </param>
        /// <param name="context">
        ///     The <see cref="StreamingContext"/> value that gives context for
        ///     the source and/or destination.
        /// </param>
        public InvalidContentException(SerializationInfo info, StreamingContext context)
        {
            Name = (string?)info.GetValue("name", typeof(string));
            Value = (T?)info.GetValue("value", typeof(T));
        }

        /// <summary>
        ///     Adds the values needed for serialization to the
        ///     <see cref="SerializationInfo"/> class instance provided.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo"/> class instance to provide
        ///     values to that are needed for serialization.
        /// </param>
        /// <param name="context">
        ///     The <see cref="StreamingContext"/> value that gives context for
        ///     the source and/or destination.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("name", Name, typeof(string));
            info.AddValue("value", Value, typeof(T));
        }
    }
}
