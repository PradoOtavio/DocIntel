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

namespace DocIntel.WebApp.Areas.API.Models
{
    public class APIAppUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }

        public DateTime LastActivity { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool Enabled { get; set; }

        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) & string.IsNullOrEmpty(LastName))
                    return UserName;
                return $"{FirstName} {LastName}";
            }
        }
    }
}