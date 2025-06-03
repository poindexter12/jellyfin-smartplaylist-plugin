using System.Collections.Generic;
using Jellyfin.Plugin.SmarterPlaylist;
using Jellyfin.Plugin.SmarterPlaylist.QueryEngine;
using Xunit;

namespace Jellyfin.Plugin.SmarterPlaylist.Tests
{
    public class SmarterPlaylistTest
    {
        [Fact]
        public void DtoToSmarterPlaylist()
        {
            var dto = new SmarterPlaylistDto
            {
                Id = "87ccaa10-f801-4a7a-be40-46ede34adb22",
                Name = "Foo",
                User = "Rob"
            };

            var es = new ExpressionSet
            {
                Expressions = new List<Expression>
                {
                    new("foo", "bar", "biz")
                }
            };
            dto.ExpressionSets = new List<ExpressionSet>
            {
                es
            };
            dto.Order = new OrderDto
            {
                Name = "Release Date Descending"
            };
            var SmarterPlaylist = new SmarterPlaylist(dto);

            Assert.Equal(1000, SmarterPlaylist.MaxItems);
            Assert.Equal("87ccaa10-f801-4a7a-be40-46ede34adb22", SmarterPlaylist.Id);
            Assert.Equal("Foo", SmarterPlaylist.Name);
            Assert.Equal("Rob", SmarterPlaylist.User);
            Assert.Equal("foo", SmarterPlaylist.ExpressionSets[0].Expressions[0].MemberName);
            Assert.Equal("bar", SmarterPlaylist.ExpressionSets[0].Expressions[0].Operator);
            Assert.Equal("biz", SmarterPlaylist.ExpressionSets[0].Expressions[0].TargetValue);
            Assert.Equal("PremiereDateOrderDesc", SmarterPlaylist.Order.GetType().Name);
        }
    }
}