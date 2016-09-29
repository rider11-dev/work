﻿using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneCardSln.Components.Extensions;

namespace OneCardSln.Repository.Card
{
    public class CardOperationRepository
    {
        static Dictionary<string, string> _dataSrc;

        public Dictionary<string, string> DataSrc
        {
            get
            {
                if (_dataSrc == null)
                {
                    _dataSrc = EnumExtension.ToDict<CardOperation>();
                }

                return _dataSrc;
            }
        }
    }
}
