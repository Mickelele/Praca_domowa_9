﻿namespace APBD8.Models.DTO_s;

public class PaginatedResponse
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripDTO> Trips { get; set; }
}