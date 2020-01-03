using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Infrastructure.Mappers.Base {

    /// <summary>
    /// Generic converter for IPagedList collections
    /// </summary>
    /// <typeparam name="TMapFrom"></typeparam>
    /// <typeparam name="TMapTo"></typeparam>
    public class PagedListConverter<TMapFrom, TMapTo> : ITypeConverter<IPagedList<TMapFrom>, IPagedList<TMapTo>> {

        /// <summary>Performs conversion from source to destination type</summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public IPagedList<TMapTo> Convert(IPagedList<TMapFrom> source, IPagedList<TMapTo> destination, ResolutionContext context) {
            if (source == null) return null;
            var vm = source.Items.Select(m => context.Mapper.Map<TMapFrom, TMapTo>(m)).ToList();


             var pagedList = PagedList.From<TMapTo, TMapFrom>(source, (con)=> context.Mapper.Map<IEnumerable<TMapTo>>(con));
            // var pagedList = vm.ToPagedList(source.PageIndex, source.PageSize);
            return pagedList;
        }
    }
}