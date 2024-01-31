﻿using R34Sharp.Models;
using R34Sharp.Tools;

using System;
using System.Linq;

namespace R34Sharp.Search
{
    /// <summary>
    /// A search builder for Rule34 Posts.
    /// </summary>
    public sealed class R34PostsSearchBuilder
    {
        /// <summary>
        /// The limit of posts the API should return.
        /// </summary>
        /// <remarks>
        /// The value must be between 1 and 1000 posts.
        /// </remarks>
        public int Limit
        {
            get => this._limit;
            set
            {
                if (value < 1 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The limit must be between 1 and 1000.");
                }

                this._limit = value;
            }
        }

        /// <summary>
        /// The Id of a specific Rule34 post.
        /// </summary>
        /// <remarks>
        /// This field is an optional value and if filled in, only one post will be returned.
        /// </remarks>
        public Optional<ulong> Id { get; set; }

        /// <summary>
        /// Get a specific offset of posts from a given number.
        /// </summary>
        /// <remarks>
        /// To find a specific set of posts in a search, you can use "offsets" to find the posts following the number of offsets already searched. For example, if you've searched the last 1000 posts and want to get the next posts without overloading the search, you can set the number of "offsets" to "1" and so on. It's important to remember that "offsets" are directly related to the number of posts that will be searched. <br/><br/>
        /// If this value is filled in, pay attention to the <see cref="R34TagModel"/> of the search, as there may be inconsistency.
        /// </remarks>
        public Optional<int> Offset { get; set; }

        /// <summary>
        /// The tags that will be used for the search.
        /// </summary>
        public R34TagModel[] Tags
        {
            get => this._tags;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("Search tags are missing or empty.", nameof(value));
                }

                this._tags = value;
            }
        }

        /// <summary>
        /// The tags that will be ignored when searching for Posts.
        /// </summary>
        public R34TagModel[] BlockedTags { get => this._blockedTags; set => this._blockedTags = value; }

        private int _limit;
        private R34TagModel[] _tags;
        private R34TagModel[] _blockedTags;

        /// <summary>
        /// Build a custom search for Rule34 Posts.
        /// </summary>
        public R34PostsSearchBuilder()
        {
            _ = WithLimit(100);
            _ = WithTags(Array.Empty<R34TagModel>());
            _ = WithBlockedTags(Array.Empty<R34TagModel>());

            this.Id = new();
            this.Offset = new();
        }

        internal string GetTagsString()
        {
            return ConvertTagsToString(this._tags);
        }
        internal string GetBlockedTagsString()
        {
            return ConvertTagsToString(this._blockedTags);
        }

        /// <summary>
        /// Set the post search limit.
        /// </summary>
        /// <param name="value">The limit value.</param>
        /// <returns>This search builder.</returns>
        public R34PostsSearchBuilder WithLimit(int value)
        {
            this.Limit = value;
            return this;
        }

        /// <summary>
        /// Set required Post Id.
        /// </summary>
        /// <param name="value">The id value.</param>
        /// <returns>This search builder.</returns>
        public R34PostsSearchBuilder WithId(ulong value)
        {
            this.Id = new(value);
            return this;
        }

        /// <summary>
        /// Set the Search Offset value.
        /// </summary>
        /// <param name="value">The offset value.</param>
        /// <returns>This search builder.</returns>
        public R34PostsSearchBuilder WithOffset(int value)
        {
            this.Offset = new(value);
            return this;
        }

        /// <summary>
        /// Define the tags that will be used in the search.
        /// </summary>
        /// <param name="tags">The tags collection.</param>
        /// <returns>This search builder.</returns>
        public R34PostsSearchBuilder WithTags(R34TagModel[] tags)
        {
            this.Tags = tags;
            return this;
        }

        /// <summary>
        /// Define the blocked tags that will be ignored in the search.
        /// </summary>
        /// <param name="tags">The tags collection.</param>
        /// <returns>This search builder.</returns>
        public R34PostsSearchBuilder WithBlockedTags(R34TagModel[] tags)
        {
            this.BlockedTags = tags;
            return this;
        }

        private static string ConvertTagsToString(R34TagModel[] tags)
        {
            return string.Join('+', tags.Where(tag => tag != null).Select(tag => tag.Name));
        }
    }
}
