﻿using GoogleCloudPrintApi.Attributes;
using System;
using System.Collections.Generic;

namespace GoogleCloudPrintApi.Models.Printer
{
    /// <summary>
    /// Parameters for /update interface, capabilities, semantic_state & semantic_state_diff are partially supported only
    /// </summary>
    public class UpdateRequest
    {
        /// <summary>
        /// Unique printer identification (generated by Google Cloud Print).
        /// </summary>
        [Required]
        public string PrinterId { get; set; }

        /// <summary>
        /// System name of the printer (need not be unique). (optional)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the printer to be displayed to users. If a default display name is not provided, the system name is displayed. Providing this parameter as the empty string clears any existing default display name. (optional)
        /// </summary>
        public string DefaultDisplayName { get; set; }

        /// <summary>
        /// Display name of the printer customized by an authenticated user. This name is only displayed to the user who provided it; all other users see the default display name. Providing this parameter as the empty string clears any existing customized display name. (optional, for use by GCP clients only)
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Identification of the printer client or proxy (many printers can share the same proxy). (optional)
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        /// Manufacturer-provided serial number of the printer. (optional)
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// Name of the printer manufacturer. (optional)
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Printer model. (optional)
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Version of the GCP protocol supported by the printer, e.g. "2.0". (optional)
        /// </summary>
        public string GCPVersion { get; set; }

        /// <summary>
        /// URL with printer setup instructions. (optional)
        /// </summary>
        public string SetupUrl { get; set; }

        /// <summary>
        /// URL that user should be directed to if they are in need of printer support. (optional)
        /// </summary>
        public string SupportUrl { get; set; }

        /// <summary>
        /// URL that user should be directed to if printer needs a firmware update. (optional)
        /// </summary>
        public string UpdateUrl { get; set; }

        /// <summary>
        /// Version of the printer's firmware. (optional)
        /// </summary>
        public string Firmware { get; set; }

        /// <summary>
        /// Current (provided or confirmed by the printer) or pending (provided by a client) local settings of the printer (see Local settings). (optional)
        /// </summary>
        public LocalSettings LocalSettings { get; set; }

        /// <summary>
        /// Current state of the printer in CDS format; overwrites the CDS stored for the printer. (optional, mutually exclusive with semantic_state_diff)
        /// </summary>
        [MutuallyExclusiveTo("SemanticStateDiff")]
        public dynamic SemanticState { get; set; }

        /// <summary>
        /// Diff on the CDS stored for the printer; interpreted as described in CDS Diff Interpretation. (optional, mutually exclusive with semantic_state)
        /// </summary>
        [MutuallyExclusiveTo("SemanticState")]
        public dynamic SemanticStateDiff { get; set; }

        /// <summary>
        /// Set this to "true" if the capabilities are provided in CDD format. (required to be true for GCP 2.0 if capabilities are provided)
        /// </summary>
        public bool UseCdd { get; set; }

        /// <summary>
        /// Printer capabilities (XPS, PPD or CDD). (optional)
        /// </summary>
        public string Capabilities { get; set; }

        /// <summary>
        /// Printer default settings (XPS only). (optional)
        /// </summary>
        public string Defaults { get; set; }

        /// <summary>
        /// A hash or digest value of the capabilities data. This value is useful, for example, to compare values and check whether the local printer's capabilities have changed. (optional)
        /// </summary>
        public string CapsHash { get; set; }

        /// <summary>
        /// Tags (free-form string values) to add to the printer. You can attach a set of unique tags to a printer, which may be useful to store additional metadata about the printer for later use by your application. (optional, repeated parameter)
        /// </summary>
        public List<string> Tag { get; set; }

        /// <summary>
        /// Can be used to remove existing tags from the printer. Each existing tag that matches at least one of the patterns* provided for this parameter will be removed. (optional, repeated parameter)
        /// </summary>
        public List<string> RemoveTag { get; set; }

        /// <summary>
        /// Private data to add to the printer. Private data values are similar to tags except that they are write-only; they can be added in /register and added or removed in /update, but they are never rendered in responses from the server. (optional, repeated parameter)
        /// </summary>
        public List<string> Data { get; set; }

        /// <summary>
        /// Can be used to remove existing private data from the printer. Each existing private data string that matches at least one of the patterns* provided for this parameter will be removed. (optional, repeated parameter)
        /// </summary>
        public List<string> RemoveData { get; set; }

        /// <summary>
        /// System name of the printer (need not be unique).
        /// </summary>
        [Obsolete]
        public string Printer { get; set; }

        /// <summary>
        /// Descriptive string about the printer. Providing this parameter as the empty string clears any existing description.
        /// </summary>
        [Obsolete]
        public string Description { get; set; }

        /// <summary>
        /// Comma-separated list of content types that printer supports.
        /// </summary>
        [Obsolete]
        public string ContentTypes { get; set; }
    }
}