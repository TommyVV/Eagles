﻿using System;
using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.Scroll;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ScrollAccess;
using Eagles.Application.Model.Scroll.GetScrollImg;
using Eagles.Application.Model.Scroll.GetScrollNew;

namespace Eagles.DomainService.Core.Scroll
{
    public class ScrollHandler : IScrollHandler
    {
        private readonly IScrollAccess iScrollAccess;
        private readonly IUtil util;

        public ScrollHandler(IScrollAccess iScrollAccess, IUtil util)
        {
            this.iScrollAccess = iScrollAccess;
            this.util = util;
        }

        public GetScrollImgResponse GetScrollImg(GetScrollImgRequest request)
        {
            var response = new GetScrollImgResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iScrollAccess.GetScrollImg(request.PageType);
            if (result != null && result.Count > 0)
            {
                response.RollImgUrl = result?.Select(x => x.ImageUrl).ToList();
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request)
        {
            var response = new GetScrollNewsResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");

            var nowDate = DateTime.Now.ToString("yyyyMMdd");
            var date = DateTime.Now.ToString("MMdd");
            var result = iScrollAccess.GetScrollNews(nowDate,date);

            if (result != null && result.Any() )
            {
                response.SystemNewsList = result.Select(x => new SystemNews()
                {
                    NewsId = x.NewsId,
                    NewsName = x.NewsName,
                    NewsContent = x.NewsContent
                }).ToList();
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}