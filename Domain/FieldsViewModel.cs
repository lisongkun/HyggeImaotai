﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using hygge_imaotai.Entity;

namespace hygge_imaotai.Domain
{
    /// <summary>
    /// 用户管理 - 搜索的condition
    /// </summary>
    public class FieldsViewModel:ViewModelBase
    {
        private string? _phone;
        private string? _userId;
        private string? _province;
        private string? _city;


        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string? UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string? Province
        {
            get => _province;
            set => SetProperty(ref _province, value);
        }

        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public static ObservableCollection<UserEntity> SearchResult { get; } =
            new ObservableCollection<UserEntity>()
            {
                new UserEntity(true,1126643307,"18632081130","eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJtdCIsImV4cCI6MTY5MTU4MzcyOCwidXNlcklkIjoxMTI2NjQzMzA3LCJkZXZpY2VJZCI6IjJGMjA3NUQwLUI2NkMtNDI4Ny1BOTAzLURCRkY2MzU4MzQyQSIsImlhdCI6MTY4ODk5MTcyOH0.AyRpWs53Wd9QqPXgwLdTRix9UJqbfYnLaTzbIsPu0LA",
                    "","","","","","",0,"",DateTime.Now, DateTime.Now.AddDays(30))
            };

        public bool? IsAllItems1Selected
        {
            get
            {
                var selected = SearchResult.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, SearchResult);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<UserEntity> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
    }
}
