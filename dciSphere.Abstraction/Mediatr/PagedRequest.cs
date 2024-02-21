﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Mediatr;
public record PagedRequest<T> : IPagedRequest<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
