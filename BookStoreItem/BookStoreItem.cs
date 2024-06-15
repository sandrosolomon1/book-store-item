[assembly: CLSCompliant(true)]

namespace BookStoreItem
{
    /// <summary>
    /// Represents an item in a book store.
    /// </summary>
    public class BookStoreItem
    {
        private readonly string authorName;
        private readonly string? isni;
#pragma warning disable CA1805 // Do not initialize unnecessarily
        private readonly bool hasIsni = false;
#pragma warning restore CA1805 // Do not initialize unnecessarily

        private decimal price = 0.00m;
        private string currency;
        private int amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BookStoreItem(string authorName, string title, string publisher, string isbn)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException(null, nameof(authorName));
            }
            else if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(null, nameof(title));
            }
            else if (string.IsNullOrWhiteSpace(publisher))
            {
                throw new ArgumentException(null, nameof(publisher));
            }

            this.authorName = authorName;
            this.Title = title;
            this.Publisher = publisher;

            this.BookBinding ??= string.Empty;
            this.Currency ??= "USD";

#pragma warning disable CA1062 // Validate arguments of public methods
            if (!ValidateIsbnFormat(isbn) || !ValidateIsbnChecksum(isbn))
            {
                throw new ArgumentException(null, nameof(isbn));
            }
#pragma warning restore CA1062 // Validate arguments of public methods
            this.Isbn = isbn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="isni">A book author's ISNI.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        public BookStoreItem(string authorName, string isni, string title, string publisher, string isbn)
            : this(authorName, title, publisher, isbn)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (!ValidateIsni(isni))
            {
                throw new ArgumentException(null, nameof(isni));
            }
#pragma warning restore CA1062 // Validate arguments of public methods

            this.hasIsni = true;
            this.isni = isni;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        /// <param name="published">A book publishing date.</param>
        /// <param name="bookBinding">A book binding type.</param>
        /// <param name="price">An amount of money that a book costs.</param>
        /// <param name="currency">A price currency.</param>
        /// <param name="amount">An amount of books in the store's stock.</param>
        public BookStoreItem(string authorName, string title, string publisher,
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
                         string isbn, DateTime? published,
#pragma warning restore SA1117 // Parameters should be on same line or separate lines
                         string bookBinding, decimal price,
                         string currency, int amount)
            : this(authorName, title, publisher, isbn)
        {
            this.Published = published;
            this.BookBinding = bookBinding ?? string.Empty;

#pragma warning disable CA1062 // Validate arguments of public methods
            ThrowExceptionIfCurrencyIsNotValid(currency, nameof(currency));
#pragma warning restore CA1062 // Validate arguments of public methods
            this.Currency = currency ?? "USD";

            this.Price = price;
            this.Amount = amount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookStoreItem"/> class with the specified <paramref name="authorName"/>, <paramref name="isni"/>, <paramref name="title"/>, <paramref name="publisher"/> and <paramref name="isbn"/>, <paramref name="published"/>, <paramref name="bookBinding"/>, <paramref name="price"/>, <paramref name="currency"/> and <paramref name="amount"/>.
        /// </summary>
        /// <param name="authorName">A book author's name.</param>
        /// <param name="isni">A book author's ISNI.</param>
        /// <param name="title">A book title.</param>
        /// <param name="publisher">A book publisher.</param>
        /// <param name="isbn">A book ISBN.</param>
        /// <param name="published">A book publishing date.</param>
        /// <param name="bookBinding">A book binding type.</param>
        /// <param name="price">An amount of money that a book costs.</param>
        /// <param name="currency">A price currency.</param>
        /// <param name="amount">An amount of books in the store's stock.</param>
        public BookStoreItem(string authorName, string isni, string title, string publisher,
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
                         string isbn, DateTime? published,
#pragma warning restore SA1117 // Parameters should be on same line or separate lines
                         string bookBinding, decimal price,
                         string currency, int amount)
            : this(authorName, title, publisher, isbn, published, bookBinding, price, currency, amount)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            if (!ValidateIsni(isni))
            {
                throw new ArgumentException(null, nameof(isni));
            }
#pragma warning restore CA1062 // Validate arguments of public methods
            this.hasIsni = true;
            this.isni = isni;
        }

        /// <summary>
        /// Gets a book author's name.
        /// </summary>
        public string AuthorName => this.authorName;

        /// <summary>
        /// Gets an International Standard Name Identifier (ISNI) that uniquely identifies a book author.
        /// </summary>
        public string? Isni => this.isni;

        /// <summary>
        /// Gets a value indicating whether an author has an International Standard Name Identifier (ISNI).
        /// </summary>
        public bool HasIsni => this.hasIsni;

        /// <summary>
        /// Gets a book title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a book publisher.
        /// </summary>
        public string Publisher { get; private set; }

        /// <summary>
        /// Gets a book International Standard Book Number (ISBN).
        /// </summary>
        public string Isbn { get; private set; }

        /// <summary>
        /// Gets or sets a book publishing date.
        /// </summary>
        public DateTime? Published { get; set; }

        /// <summary>
        /// Gets or sets a book binding type.
        /// </summary>
        public string BookBinding { get; set; }

        /// <summary>
        /// Gets or sets an amount of money that a book costs.
        /// </summary>
        public decimal Price
        {
            get => this.price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(null, "price");
                }

                this.price = value;
            }
        }

        /// <summary>
        /// Gets or sets a price currency.
        /// </summary>
        public string Currency { get => this.currency; set => this.currency = value; }

        /// <summary>
        /// Gets or sets an amount of books in the store's stock.
        /// </summary>
        public int Amount
        {
            get => this.amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Amount");
                }

                this.amount = value;
            }
        }

        /// <summary>
        /// Gets a <see cref="Uri"/> to the contributor's page at the isni.org website.
        /// </summary>
        /// <returns>A <see cref="Uri"/> to the contributor's page at the isni.org website.</returns>
#pragma warning disable CA1024 // Use properties where appropriate
        public Uri GetIsniUri()
#pragma warning restore CA1024 // Use properties where appropriate
        {
            if (this.Isni != null)
            {
                return new Uri($"https://isni.org/isni/{this.Isni}");
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets an <see cref="Uri"/> to the publication page on the isbnsearch.org website.
        /// </summary>
        /// <returns>an <see cref="Uri"/> to the publication page on the isbnsearch.org website.</returns>
#pragma warning disable CA1024 // Use properties where appropriate
        public Uri GetIsbnSearchUri() => new Uri($"https://isbnsearch.org/isbn/{this.Isbn}");
#pragma warning restore CA1024 // Use properties where appropriate

        /// <summary>
        /// Returns the string that represents a current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string result;
            if (!this.HasIsni)
            {
                result = $"{this.Title}, {this.AuthorName}, ISNI IS NOT SET, {this.Price} {this.Currency}, {this.Amount}";
            }
            else
            {
                result = $"{this.Title}, {this.AuthorName}, {this.Isni}, {this.Price} {this.Currency}, {this.Amount}";
            }

            return result;
        }

        private static bool ValidateIsni(string isni)
        {
            return isni.All(c => c == 'X' || char.IsDigit(c)) && isni.Length == 16;
        }

        private static bool ValidateIsbnFormat(string isbn)
        {
            return isbn.All(c => c == 'X' || char.IsDigit(c)) && isbn.Length == 10;
        }

        private static bool ValidateIsbnChecksum(string isbn)
        {
            var checksum = 0;

            for (int i = 0; i < 10; i++)
            {
                if (isbn[i] == 'X')
                {
                    checksum += 10 * (10 - i);
                }
                else if (char.IsDigit(isbn[i]))
                {
                    checksum += (int)char.GetNumericValue(isbn[i]) * (10 - i);
                }
            }

            return checksum % 11 == 0;
        }

        private static void ThrowExceptionIfCurrencyIsNotValid(string currency, string parameterName)
        {
            if (currency.Length != 3 || !currency.All(char.IsLetter))
            {
                throw new ArgumentException(null, parameterName);
            }
        }
    }
}
