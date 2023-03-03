/* DocIntel
 * Copyright (C) 2018-2023 Belgian Defense, Antoine Cailliau, Kevin Menten
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

using System.ComponentModel.DataAnnotations;

namespace DocIntel.Core.Utils.Search.Sources
{
    public enum SourceSortCriteria
    {
        [Display(Name = "Relevance")] Relevance = 0,
        [Display(Name = "Title (A-Z)")] Title = 1,
        [Display(Name = "Last update")] LastUpdate = 2,
        [Display(Name = "Documents count")] DocumentsCount = 3
    }
}