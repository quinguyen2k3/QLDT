using Microsoft.Extensions.Caching.Memory;
using QLDT.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Cache
{
    public class PermissionCache
    {
        private readonly IMemoryCache _cache;
        private readonly PermissionRepo _permissionRepository;

        public PermissionCache(IMemoryCache cache, PermissionRepo userRepository)
        {
            _cache = cache;
            _permissionRepository = userRepository;
        }

        public async Task<List<string>> GetPermissionsAsync(long userId)
        {
            var cacheKey = $"permissions_{userId}";
            if (_cache.TryGetValue(cacheKey, out List<string> permissions))
            {
                return permissions;
            }

            permissions = (await _permissionRepository.GetAllByUserIdAsync(userId))
                .Select(p => p.Name)
                .ToList();
            _cache.Set(cacheKey, permissions, TimeSpan.FromHours(1));
            return permissions;
        }

        public void RemovePermissionsAsync(long userId)
        {
            var cacheKey = $"permissions_{userId}";
            _cache.Remove(cacheKey);
        }
    }
}