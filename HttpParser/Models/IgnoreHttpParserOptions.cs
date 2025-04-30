namespace HttpParser.Models
{
    public class IgnoreHttpParserOptions
    {
        /// <summary>
        /// Regex pattern list để so khớp và bỏ qua URL.
        /// 
        /// Ví dụ:
        ///   "^/api/health$"       -> bỏ qua URL chính xác là /api/health
        ///   "^/admin/.*"          -> bỏ qua mọi URL bắt đầu bằng /admin/
        /// 
        /// Quy tắc:
        ///   - Bắt đầu pattern với ^ nếu cần khớp từ đầu chuỗi.
        ///   - Kết thúc bằng $ nếu cần khớp chính xác toàn bộ.
        ///   - Dùng .* để khớp bất kỳ đoạn nào.
        /// </summary>
        public List<string> IgnoreUrls { get; set; } = new();

        /// <summary>
        /// Regex pattern list để xoá các header theo tên.
        /// 
        /// Ví dụ:
        ///   "^Authorization$"     -> xoá header Authorization
        ///   "^X-.*"               -> xoá mọi header bắt đầu bằng X-
        /// 
        /// Quy tắc:
        ///   - So khớp theo tên header, không phân biệt hoa thường.
        /// </summary>
        public List<string> IgnoreHeaders { get; set; } = new();

        /// <summary>
        /// Regex pattern list để xoá các cookie theo tên.
        /// 
        /// Ví dụ:
        ///   "^sessionid$"         -> xoá cookie tên sessionid
        ///   "^(temp|debug)_.*"    -> xoá mọi cookie bắt đầu bằng temp_ hoặc debug_
        /// </summary>
        public List<string> IgnoreCookies { get; set; } = new();

        /// <summary>
        /// Regex pattern list để xoá body nếu nội dung khớp.
        /// 
        /// Ví dụ:
        ///   "password=.*"         -> nếu body có chứa password thì xoá
        ///   ".*secret.*"          -> nếu có chứa từ secret bất kỳ đâu thì xoá
        /// 
        /// Quy tắc:
        ///   - Áp dụng cho raw body string, không phân biệt hoa thường.
        /// </summary>
        public List<string> IgnoreRequestBodies { get; set; } = new();
    }
}
