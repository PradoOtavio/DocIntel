/* DocIntel
 * Copyright (C) 2018-2023 Belgian Defense, Antoine Cailliau
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace DocIntel.Core.Repositories.Query
{
    public class TagQuery
    {
        public string Label { get; set; }
        public string FullLabel { get; set; }
        public string FacetPrefix { get; set; }
        public string StartsWith { get; set; }
        public Guid? FacetId { get; set; }
        public string SubscribedUser { get; set; }

        public string URL { get; set; }

        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 10;
        public Guid[] Ids { get; set; }
    }
}