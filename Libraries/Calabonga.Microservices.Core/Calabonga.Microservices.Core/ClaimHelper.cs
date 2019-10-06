using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using Calabonga.Microservices.Core.Exceptions;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Claim Helper
    /// </summary>
    public class ClaimsHelper
    {
        /// <summary>
        /// Return Claim collection from domain model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="additionalClaims"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> CreateClaims<T>(T entity, IEnumerable<Claim> additionalClaims = null) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = new List<Claim>();
            if (additionalClaims != null)
            {
                result.AddRange(additionalClaims);
            }

            var properties = typeof(T).GetProperties().Where(t => t.PropertyType.IsPrimitive
                                                                  || t.PropertyType.IsValueType
                                                                  || (t.PropertyType == typeof(string)));
            var items = from property in properties
                        let value = property.GetValue(entity)
                        where value != null
                        select new Claim(property.Name, value?.ToString());
            result.AddRange(items);
            return result;
        }

        /// <summary>
        /// Returns value from claim converted to T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="claimName"></param>
        /// <returns></returns>
        public static T GetValue<T>(ClaimsIdentity identity, string claimName)
        {
            var claim = identity.FindFirst(x => x.Type == claimName);
            if (claim == null)
            {
                return default(T);
            }

            if (string.IsNullOrWhiteSpace(claim.Value))
            {
                return default(T);
            }

            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
            }
            catch (Exception exception)
            {
                throw new MicroserviceInvalidCastException($"{claim.Value} from {claim.Value} to {typeof(T)}", exception);
            }
        }

        /// <summary>
        /// Returns a set of claims
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="claimName"></param>
        /// <returns></returns>
        public static List<T> GetValues<T>(ClaimsIdentity items, string claimName)
        {
            var result = new List<T>();
            var claims = items.FindAll(x => x.Type == claimName);
            var enumerable = claims.ToList();
            if (!enumerable.Any())
            {
                return result;
            }

            enumerable.ToList().ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x.Value))
                {
                    return;
                }

                try
                {
                    var item = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(x.Value);
                    result.Add(item);
                }
                catch (Exception exception)
                {
                    throw new MicroserviceInvalidCastException($"{x.Value} from {x.Value} to {typeof(T)}", exception);
                }
            });
            return result;
        }

        private static Claim FindFirstOrEmpty(IEnumerable<Claim> claims, string claimType)
        {
            return claims.FirstOrDefault(x => x.Value == claimType);
        }

    }
}
