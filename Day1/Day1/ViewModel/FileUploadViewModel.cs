﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Day1.ViewModel
{
    public class FileUploadViewModel : BaseViewModel
    {
        public HttpPostedFileBase fileUpload { get; set; }
    }
}