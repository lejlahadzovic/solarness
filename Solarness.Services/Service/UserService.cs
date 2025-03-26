using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solarness.Model;
using Solarness.Model.Requests;
using Solarness.Model.SearchObjects;
using Solarness.Services.Database;
using Solarness.Services.Interface;
using StudentOglasi.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solarness.Services.Service
{
    public class UserService : BaseCRUDService<Model.User, Database.User, UserSearchObject,UserInsertRequest,UserUpdateRequest>, IUserService
    {
        public UserService(SolarnessDbContext context, IMapper mapper):base(context,mapper)
        {
        }
        public override async System.Threading.Tasks.Task BeforeInsert(Database.User entity, UserInsertRequest insert)
        {
            entity.PasswordSalt = GenerateSalt();
            entity.PasswordSalt = GenerateHash(entity.PasswordSalt, insert.Password);

            entity.Role = _context.Roles.SingleOrDefault(x => x.Name == "Student")!;

            if (entity.Role == null)
            {
                throw new Exception("Uloga 'Student' ne postoji u bazi podataka.");
            }

            entity.RoleId = entity.Role.RoleId;
        }
       
        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        public override IQueryable<Database.User> AddInclude(IQueryable<Database.User> query, UserSearchObject? search = null)
        {
                query = query.Include(r=>r.Role);
            
            return base.AddInclude(query, search);
        }
        public async Task<Model.User> Login(string username, string password)
        {
            var entity = await _context.Users.Include("Role").FirstOrDefaultAsync(x => x.Username == username);
            if (entity == null)
            {
                return null;
            }
            var hash = GenerateHash(entity.PasswordSalt, password);
            if (hash != entity.PasswordHash)
            {
                return null;
            }
            return _mapper.Map<Model.User>(entity);
        }
        public async Task<Model.User> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("User is not authorized");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(s => s.Username == username);

            if (user == null)
            {
                throw new Exception("Student not found");
            }

            return _mapper.Map<Model.User>(user);
        }
        public bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            var hash = GenerateHash(storedSalt, inputPassword);
            return hash == storedHash;
        }
    }
}
