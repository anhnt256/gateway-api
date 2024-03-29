﻿using Dapper;
using GateWayAPI.Areas.Admin.IRepository;
using GateWayAPI.DataAccessLayer;
using GateWayAPI.Models.GateWay.Staff;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Repository
{
    public class StaffRepository : SqlRepository, IStaffRepository
    {
        public StaffRepository(string connectionString) : base(connectionString) { }

        public Staff Authenticate(string username, string password)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "select * from Staff where UserName = @UserName AND PasswordHash = @PasswordHash AND IsActive = 1";
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@UserName", username, DbType.String);
                _params.Add("@PasswordHash", password, DbType.String);
                var user = conn.QueryFirstOrDefault<Staff>(sql, _params);
                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("GeneralSetting")["Secret"];
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.IsAdmin.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
        }

        public bool ChangePassword(string username, string password)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "update Staff Set PasswordHash = @PasswordHash Where UserName = @UserName";
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@UserName", username, DbType.String);
                _params.Add("@PasswordHash", password, DbType.String);
                return conn.Execute(sql, _params) == 1;
            }
        }
    }
}
