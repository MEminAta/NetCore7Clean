﻿namespace Application.DynamicQuery;

public class Dynamic
{
    public IEnumerable<Sort>? Sort { get; set; }
    public Filter? Filter { get; set; }
}