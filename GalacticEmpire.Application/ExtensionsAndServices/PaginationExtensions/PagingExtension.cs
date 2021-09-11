﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.PaginationExtensions
{
    public static class PagingExtension
    {
        public static async Task<PagedResult<T>> ToPagedList<T>(this IQueryable<T> list, int pagesize, int pagenumber)
        {
            PagedResult<T> result = new PagedResult<T>(
                await list.Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToListAsync(),
                await list.CountAsync(),
                pagenumber,
                pagesize
            );
            return result;
        }
    }
}