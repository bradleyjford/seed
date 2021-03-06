﻿using System;

namespace Seed.Api.Admin.Users
{
    public class UserDetailResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public byte[] RowVersion { get; set; }
    }
}