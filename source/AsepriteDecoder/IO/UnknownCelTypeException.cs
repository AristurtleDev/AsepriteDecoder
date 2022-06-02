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
    ///     Represents an exception that occurs when attempting to read a cel
    ///     chunk but the cel type value that was read is not a known value.
    /// </summary>
    [Serializable]
    public class UnknownCelTypeException : Exception, ISerializable
    {
        /// <summary>
        ///     Gets or Sets a value that indicates the Cel type that was read.
        /// </summary>
        public ushort? Type { get; set; }

        /// <summary>
        ///     Creates a new <see cref="UnknownCelTypeException"/> class
        ///     instance.
        /// </summary>
        /// <param name="message">
        ///     A message that describes the reason the exception occurred.
        /// </param>
        /// <param name="type">
        ///     A value that indicates the cel type that was read that is
        ///     unknown.
        /// </param>
        public UnknownCelTypeException(string message, ushort type)
         : base(message) => Type = type;

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
        public UnknownCelTypeException(SerializationInfo info, StreamingContext context)
        {
            Type = (ushort?)info.GetValue("type", typeof(ushort));
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
            info.AddValue("type", Type, typeof(ushort));
        }
    }
}