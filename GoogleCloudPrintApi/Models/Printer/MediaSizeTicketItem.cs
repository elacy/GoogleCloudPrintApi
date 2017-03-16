﻿namespace GoogleCloudPrintApi.Models.Printer
{

    /// <summary>
    /// Ticket item indicating what media size to use.
    /// </summary>
    public class MediaSizeTicketItem
    {
        //public MediaSizeTicketItem(int width_microns, int height_microns, bool is_continuous_feed, string vendor_id)
        //{
        //    WidthMicrons = width_microns;
        //    HeightMicrons = height_microns;
        //    IsContinuousFeed = is_continuous_feed;
        //    VendorId = vendor_id;
        //}

        /// <summary>
        /// Width (in microns) of the media to print to.
        /// </summary>
        public int WidthMicrons { get; set; }

        /// <summary>
        /// Height (in microns) of the media to print to.
        /// </summary>
        public int HeightMicrons { get; set; }

        /// <summary>
        /// Whether the media size selection is continuously fed. If false, both width
        /// and height must be set. If true, only one need be set.
        /// </summary>
        public bool IsContinuousFeed { get; set; } = false;

        /// <summary>
        /// Vendor-provided ID of the MediaSize option. Needed to disambiguate media
        /// sizes that may have the same width and height, but may have a different
        /// effect for the printer.
        /// </summary>
        public string VendorId { get; set; }
    }

}
