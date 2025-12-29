using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Config
    {
        [Display(Name = "Mã cấu hình")]
        public int Id { get; set; }

        [Display(Name = "Mail SMTP")]
        public string? MailSmtp { get; set; }

        [Display(Name = "Mail Port")]
        public short? MailPort { get; set; }

        [Display(Name = "Mail Info")]
        public string? MailInfo { get; set; }

        [Display(Name = "Mail No-reply")]
        public string? MailNoreply { get; set; }

        [Display(Name = "Mật khẩu Email")]
        public string? MailPassword { get; set; }

        [Display(Name = "Phần đầu trang (Head)")]
        public string? PlaceHead { get; set; }

        [Display(Name = "Phần thân trang (Body)")]
        public string? PlaceBody { get; set; }

        [Display(Name = "Google ID")]
        public string? GoogleId { get; set; }

        [Display(Name = "Liên hệ")]
        public string? Contact { get; set; }

        [Display(Name = "Bản quyền")]
        public string? Copyright { get; set; }

        [Display(Name = "Tiêu đề trang")]
        public string? Title { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Từ khóa")]
        public string? Keyword { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string? Lang { get; set; }

        [Display(Name = "Hotline")]
        public string? HotLine { get; set; }

        [Display(Name = "Link YouTube")]
        public string? YoutubeLink { get; set; }

        [Display(Name = "Link Picasa")]
        public string? PicasaLink { get; set; }

        [Display(Name = "Link Flickr")]
        public string? FlickrLink { get; set; }

        [Display(Name = "Mạng xã hội 1")]
        public string? SocialLink1 { get; set; }

        [Display(Name = "Mạng xã hội 2")]
        public string? SocialLink2 { get; set; }

        [Display(Name = "Mạng xã hội 3")]
        public string? SocialLink3 { get; set; }

        [Display(Name = "Mạng xã hội 4")]
        public string? SocialLink4 { get; set; }

        [Display(Name = "Mạng xã hội 5")]
        public string? SocialLink5 { get; set; }

        [Display(Name = "Mạng xã hội 6")]
        public string? SocialLink6 { get; set; }

        [Display(Name = "Mạng xã hội 7")]
        public string? SocialLink7 { get; set; }

        [Display(Name = "Mạng xã hội 8")]
        public string? SocialLink8 { get; set; }

        [Display(Name = "Mạng xã hội 9")]
        public string? SocialLink9 { get; set; }
    }
}
