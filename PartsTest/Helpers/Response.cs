using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.Helpers
{
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
     
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
   
        public object ErrorMessage { get; set; } = null;


    }


}