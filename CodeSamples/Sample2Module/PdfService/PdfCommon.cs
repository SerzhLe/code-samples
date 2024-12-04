using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using NodaTime;
using System.Globalization;

namespace CodeSamples.Sample2Module.PdfService
{
    public static class PdfCommon
    {
        public const double A4WidthMm = 210.0;
        public const double A4HeightMm = 297.0;
        public const double MarginTopMm = 13.0;
        public const double MarginSideMm = 5.0;
        public const double A4WidthWithoutMargin = A4WidthMm - MarginSideMm * 2.0;
        public const double A4AlbumWidthWithoutMargin = A4HeightMm - MarginSideMm * 2.0;

        // assuming that 1rem equals 16px
        public const double MmInRem = 4.2333333328;

        // Font Styles
        public const string BodyFontStyle = "Normal";
        public const string H2FontStyle = "PDF-h2";
        public const string H2BoldFontStyle = "PDF-h2-bold";
        public const string H3FontStyle = "PDF-h3";
        public const string BodyBoldFontStyle = "PDF-body-bold";
        public const string BodySFontStyle = "PDF-body-s";

        // Colors
        public static readonly Color Neutral600 = new Color(24, 24, 24);
        public static readonly Color Neutral500 = new Color(51, 51, 51);
        public static readonly Color Neutral400 = new Color(82, 82, 82);
        public static readonly Color Neutral300 = new Color(153, 153, 153);

        public static readonly Color Primary100 = new Color(222, 219, 240);
        public static readonly Color Primary75 = new Color(240, 241, 249);

        public static readonly Color Critical500 = new Color(190, 68, 42);

        public static void DefineBasicStyles(Document document)
        {
            var style = document.Styles[BodyFontStyle];

            style.Font.Size = Unit(0.75);
            style.ParagraphFormat.LineSpacing = Unit(1);
            style.Font.Color = Neutral600;
            style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
            style.ParagraphFormat.SpaceAfter = Unit(0.5);

            style = document.AddStyle(H2FontStyle, BodyFontStyle);
            style.Font.Size = Unit(1);
            style.ParagraphFormat.LineSpacing = Unit(1.5);
            style.ParagraphFormat.SpaceAfter = Unit(0.75);

            style = document.AddStyle(H2BoldFontStyle, H2FontStyle);
            style.Font.Bold = true;

            style = document.AddStyle(H3FontStyle, BodyFontStyle);
            style.Font.Size = Unit(0.63);
            style.ParagraphFormat.LineSpacing = Unit(1);
            style.ParagraphFormat.SpaceAfter = Unit(0.5);
            style.Font.Bold = true;
            style.Font.Color = Neutral500;

            style = document.AddStyle(BodyBoldFontStyle, BodyFontStyle);
            style.Font.Bold = true;

            style = document.AddStyle(BodySFontStyle, BodyFontStyle);
            style.Font.Size = Unit(0.63);
            style.ParagraphFormat.SpaceAfter = Unit(0.5);
            style.Font.Color = Neutral400;
        }

        public static void DefineBasicLayout(Document document)
        {
            var section = document.AddSection();

            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.HeaderDistance = UnitMm(1.5);
            section.PageSetup.FooterDistance = UnitMm(0);
            section.PageSetup.TopMargin = UnitMm(MarginTopMm);
            section.PageSetup.LeftMargin = UnitMm(MarginSideMm);
            section.PageSetup.RightMargin = UnitMm(MarginSideMm);
            section.PageSetup.BottomMargin = UnitMm(MarginSideMm);
        }

        public static void DrawTableBorders(Table table, Color? color = null, bool excludeTop = false, bool excludeRight = false, bool excludeBottom = false, bool excludeLeft = false)
        {
            color ??= Neutral300;

            if (!excludeTop)
            {
                table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Top, BorderStyle.Single, Unit(0.1), color.Value);
            }

            if (!excludeRight)
            {
                table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Right, BorderStyle.Single, Unit(0.1), color.Value);
            }

            if (!excludeBottom)
            {
                table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Bottom, BorderStyle.Single, Unit(0.1), color.Value);
            }

            if (!excludeLeft)
            {
                table.SetEdge(0, 0, table.Columns.Count, table.Rows.Count, Edge.Left, BorderStyle.Single, Unit(0.1), color.Value);
            }
        }

        public static Unit Unit(double rem) => new(rem * MmInRem, UnitType.Millimeter);

        public static Unit UnitMm(double mm) => new(mm, UnitType.Millimeter);

        public static void DefineFooter(Document document)
        {
            foreach (Section section in document.Sections)
            {
                var footerParagraph = section.Footers.Primary.AddParagraph();

                footerParagraph.AddText("Page ");
                footerParagraph.AddPageField();
                footerParagraph.AddText(" of ");
                footerParagraph.AddNumPagesField();
            }
        }

        public static void DefineHeader(Document document, string authorFirstName, string authorLastName, string currentTz)
        {
            var formatedDateTime = GetFormatedTimezone(currentTz);

            foreach (Section section in document.Sections)
            {
                Table headerTable = section.Headers.Primary.AddTable();

                headerTable.Borders.Width = 0;

                if (section.PageSetup.Orientation == Orientation.Portrait)
                {
                    headerTable.AddColumn(UnitMm(60));
                    headerTable.AddColumn(UnitMm(140));
                }
                else
                {
                    headerTable.AddColumn(UnitMm(60));
                    headerTable.AddColumn(UnitMm(227));
                }

                Row row = headerTable.AddRow();
                Cell authorCell = row.Cells[0];
                Paragraph authorParagraph = authorCell.AddParagraph();

                FormattedText generatedByText = authorParagraph.AddFormattedText("Generated by: ");
                generatedByText.Font.Color = Neutral400;

                FormattedText authorNameText = authorParagraph
                    .AddFormattedText(GetFullNameOrNull(authorFirstName, authorLastName));
                authorNameText.Font.Color = Neutral600;

                Cell pdfDateTimeCreatedCell = row.Cells[1];
                Paragraph dateTimeParagraph = pdfDateTimeCreatedCell.AddParagraph();
                dateTimeParagraph.Format.Alignment = ParagraphAlignment.Right;

                FormattedText dateTimeText = dateTimeParagraph.AddFormattedText("Date&Time: ");
                dateTimeText.Font.Color = Neutral400;

                FormattedText dateTimeValue = dateTimeParagraph.AddFormattedText(formatedDateTime);
                dateTimeValue.Font.Color = Neutral600;
            }
        }

        public static string GetFormatedTimezone(string timezone)
        {
            var timezoneInfo = !string.IsNullOrWhiteSpace(timezone) ? DateTimeZoneProviders.Tzdb.GetZoneOrNull(timezone) : DateTimeZone.Utc;

            if (timezoneInfo is null)
            {
                return "Unknown timezone";
            }

            var now = Instant.FromDateTimeUtc(DateTime.UtcNow);

            var timeZoneOffset = timezoneInfo.GetUtcOffset(now);
            var currentTimeInZone = now.InZone(timezoneInfo);

            var formatedDateTime = $"{currentTimeInZone.ToString("dd MMM yyyy, HH:mm:ss", CultureInfo.InvariantCulture)} (UTC{timeZoneOffset}) {currentTimeInZone.Zone.Id}";

            return formatedDateTime;
        }

        public static string GetFullNameOrNull(string firstName, string lastName)
        {
            string fullName = string.Empty;

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                fullName += $"{firstName} ";
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                fullName += lastName;
            }

            return fullName.Trim();
        }
    }
}
