using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rescuebots
{
    class MessageBuilder
    {
        /// <summary>
        /// Marker that marks the start of a message.
        /// </summary>
        private String messageBeginMarker;

        /// <summary>
        /// Marker that marks the mark the end of a message.
        /// </summary>
        private String messageEndMarker;

        /// <summary>
        /// Buffer to store text strings.
        /// </summary>
        private String bufferedData;

        /// <summary>
        /// Create a MessageBuilder instance.
        /// </summary>
        /// <param name="messageBeginMarker">
        /// Marker that is used to find the start of a message 
        /// when trying to find messages in the buffered data.
        /// </param>
        /// <param name="messageEndMarker">
        /// Marker that is used to find the end of a message 
        /// when trying to find messages in the buffered data.
        /// </param>
        public MessageBuilder(String messageBeginMarker, String messageEndMarker)
        {
            if (messageBeginMarker == null)
            {
                throw new ArgumentNullException("messageBeginMarker");
            }

            if (messageEndMarker == null)
            {
                throw new ArgumentNullException("messageEndMarker");
            }

            this.messageBeginMarker = messageBeginMarker;
            this.messageEndMarker = messageEndMarker;

            bufferedData = "";
        }

        /// <summary>
        /// Add the data to the end of the buffered data.
        /// </summary>
        /// <param name="data">
        /// The data to add to the builder for later parsing. 
        /// If data == null, nothing is added.
        /// </param>
        public void Append(String data)
        {
            if (data != null)
            {
                bufferedData += data;
            }
        }

        /// <summary>
        /// Find and remove the next (complete) message
        /// from the buffered data (including delimiters).
        /// The beginning and the end of the message are marked 
        /// by 'messageBeginMarker' and 'messageEndMarker' 
        /// (see member variables of this class).
        /// </summary>
        /// <returns>
        /// The next (complete) message (including markers), 
        /// or null if no message was found.
        /// </returns>
        public int[,] FindAndRemoveNextMessage()
        {
            int beginIndex = bufferedData.IndexOf(messageBeginMarker);
            if (beginIndex != -1)
            {
                int endIndex = bufferedData.IndexOf(messageEndMarker, beginIndex);
                if (endIndex != -1)
                {
                    int[,] convertedstring;
                    String foundMessage = bufferedData.Substring(
                       beginIndex, (endIndex - beginIndex) + 1);
                    bufferedData = bufferedData.Substring(endIndex + 1);
                    convertedstring = StringConvertTo2DArray(foundMessage);
                    return convertedstring;
                }
            }
            return null;
        }

        public int[,] StringConvertTo2DArray(string messageonconverted)
        {
            return null;
            int lengtstring = messageonconverted.Length;
            if (lengtstring == 100)
            {
                int[,] hulp2darray = new int[10, 10];
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        return null;

                    }
                }
            }
            else
            {
                return null;
            }
        }




        public void Clear()
        {
            bufferedData = "";
        }
    }
}
