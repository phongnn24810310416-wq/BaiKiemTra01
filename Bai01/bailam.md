I. PHẦN LÝ THUYẾT VÀ CÂU HỎI NGẮN

Câu 1: Trình bày sự khác nhau giữa Value Types (Kiểu giá trị) và Reference Types (Kiểu tham chiếu) trong C# về cơ chế lưu trữ vùng nhớ (Stack vs Heap).

Trong C#, các kiểu dữ liệu được chia thành hai nhóm chính là Value Types và Reference Types. Hai nhóm này khác nhau về cách lưu trữ dữ liệu, cách sao chép giá trị và cách các biến tham chiếu đến dữ liệu trong bộ nhớ.

Value Types là những kiểu dữ liệu mà biến lưu trực tiếp giá trị của dữ liệu. Khi khai báo một biến thuộc Value Type, giá trị của biến được lưu trực tiếp trong vùng nhớ mà biến đó sử dụng.

Các kiểu dữ liệu thuộc Value Types bao gồm các kiểu số nguyên, số thực, bool, char, struct và enum.

Khi gán một biến Value Type cho một biến Value Type khác, giá trị của biến ban đầu sẽ được sao chép sang biến mới. Sau khi sao chép, hai biến có hai giá trị độc lập. Nếu giá trị của một biến thay đổi thì giá trị của biến còn lại không bị thay đổi theo.

Value Type thường được lưu trên Stack khi nó là biến cục bộ. Stack là vùng nhớ được sử dụng để lưu trữ các biến cục bộ và thông tin phục vụ quá trình thực thi phương thức.

Tuy nhiên, không phải tất cả Value Type đều luôn nằm trên Stack. Vị trí lưu trữ còn phụ thuộc vào ngữ cảnh mà Value Type được sử dụng. Nếu Value Type là thành viên của một đối tượng thì nó có thể nằm trong đối tượng được cấp phát trên Heap.

Reference Types là những kiểu dữ liệu mà biến không lưu trực tiếp dữ liệu của đối tượng. Biến sẽ lưu một tham chiếu đến nơi đối tượng được cấp phát trong bộ nhớ.

Các kiểu dữ liệu thuộc Reference Types bao gồm class, object, string, array, interface và delegate.

Đối tượng của Reference Type thường được cấp phát trên Heap. Heap là vùng nhớ dùng để lưu trữ các đối tượng trong thời gian chương trình chạy.

Khi một biến Reference Type được gán cho một biến Reference Type khác, tham chiếu đến đối tượng được sao chép. Do đó, hai biến có thể cùng tham chiếu đến một đối tượng trên Heap.

Điểm khác nhau quan trọng nhất là Value Type lưu trực tiếp giá trị, còn Reference Type lưu tham chiếu đến đối tượng.

Value Type khi sao chép sẽ tạo ra một bản sao giá trị độc lập.

Reference Type khi sao chép sẽ sao chép tham chiếu, do đó nhiều biến có thể cùng tham chiếu đến một đối tượng.

Stack thường được sử dụng cho các biến cục bộ và thông tin thực thi phương thức. Heap thường được sử dụng để lưu trữ các đối tượng của Reference Type và được quản lý bởi Garbage Collector.


Câu 2: Tính năng Init-only Properties (init) trong C# 9/10 khác gì so với thuộc tính có set thông thường? Nêu trường hợp sử dụng thực tế.

Init-only Properties là tính năng được giới thiệu trong C# 9, cho phép một thuộc tính được gán giá trị trong quá trình khởi tạo đối tượng nhưng không cho phép thay đổi giá trị sau khi đối tượng đã được khởi tạo.

Thuộc tính có set thông thường cho phép giá trị được gán trong quá trình khởi tạo và tiếp tục được thay đổi sau khi đối tượng đã được tạo.

Đối với set, việc gán giá trị có thể được thực hiện nhiều lần trong suốt thời gian đối tượng tồn tại. Điều này phù hợp với các thuộc tính mà giá trị có thể thay đổi trong quá trình chương trình hoạt động.

Đối với init, việc gán giá trị chỉ được phép thực hiện trong quá trình khởi tạo đối tượng. Sau khi quá trình khởi tạo hoàn thành, không thể gán giá trị mới cho thuộc tính đó.

Init giúp hạn chế việc thay đổi dữ liệu sau khi đối tượng đã được khởi tạo. Điều này giúp trạng thái của đối tượng dễ kiểm soát hơn và giảm khả năng dữ liệu bị thay đổi ngoài ý muốn.

Init không có nghĩa là toàn bộ đối tượng trở thành không thể thay đổi. Nó chỉ giới hạn việc gán lại đối với những thuộc tính được khai báo với init. Các thuộc tính khác vẫn có thể thay đổi nếu được phép.

Thuộc tính sử dụng init vẫn có thể được thiết lập khi tạo đối tượng. Sau khi đối tượng được tạo xong, giá trị của thuộc tính không được phép gán lại thông qua các phép gán thông thường.

Thuộc tính sử dụng set phù hợp với dữ liệu có khả năng thay đổi trong quá trình sử dụng chương trình.

Thuộc tính sử dụng init phù hợp với dữ liệu cần xác định một lần khi khởi tạo và cần giữ nguyên trong suốt thời gian sử dụng đối tượng.

Một trường hợp sử dụng thực tế của init là các thông tin định danh hoặc thông tin cấu hình được xác định khi tạo đối tượng và không muốn bị thay đổi sau đó.


Câu 3: Phân biệt sự khác nhau giữa phương thức virtual ở lớp cha và phương thức override ở lớp con khi triển khai tính Đa hình (Polymorphism).

Trong lập trình hướng đối tượng, đa hình là khả năng cho phép cùng một phương thức có thể có cách thực hiện khác nhau tùy theo loại đối tượng thực tế.

Trong C#, virtual và override được sử dụng để triển khai đa hình động. Đa hình động cho phép chương trình xác định phương thức nào cần được thực thi dựa trên đối tượng thực tế tại thời điểm chạy chương trình.

Từ khóa virtual được sử dụng khi khai báo phương thức trong lớp cha. Khi một phương thức được khai báo là virtual, lớp con có quyền ghi đè phương thức đó nếu cần thay đổi cách thực hiện.

Phương thức virtual có thể có phần thân và cách thực hiện mặc định trong lớp cha. Lớp con có thể sử dụng lại cách thực hiện của lớp cha hoặc ghi đè để cung cấp cách thực hiện riêng.

Từ khóa override được sử dụng trong lớp con để ghi đè một phương thức virtual hoặc abstract của lớp cha.

Phương thức override phải có cùng tên, cùng danh sách tham số và phù hợp với phương thức mà nó ghi đè trong lớp cha.

Khi một biến tham chiếu có kiểu lớp cha nhưng đang tham chiếu đến đối tượng của lớp con, việc gọi phương thức virtual sẽ được xử lý dựa trên kiểu đối tượng thực tế.

Điều này tạo ra tính đa hình vì cùng một lời gọi phương thức có thể dẫn đến cách thực hiện khác nhau tùy theo đối tượng.

Virtual có vai trò cho phép lớp con tùy chỉnh phương thức của lớp cha.

Override có vai trò cung cấp cách thực hiện mới cho phương thức của lớp cha tại lớp con.

Virtual được khai báo ở lớp cha, còn override được khai báo ở lớp con.

Virtual là cơ chế cho phép ghi đè, còn override là cơ chế thực hiện việc ghi đè.

Nếu lớp con không ghi đè phương thức virtual thì phương thức của lớp cha vẫn được sử dụng.

Khi lớp con ghi đè bằng override, phương thức của lớp con sẽ được sử dụng khi đối tượng thực tế thuộc lớp con.


Câu 4: Tại sao một thành phần được khai báo là static trong Class lại không thể truy xuất thông qua một thể hiện Object Instance được tạo bằng toán tử new?

Trong C#, static là từ khóa dùng để khai báo thành phần thuộc về Class thay vì thuộc về từng Object Instance.

Một Class có thể tạo ra nhiều Object Instance bằng toán tử new. Mỗi Object Instance là một đối tượng riêng và có trạng thái riêng đối với các thành phần không static.

Thanh phần static không gắn với một đối tượng cụ thể. Nó thuộc về chính Class và được dùng chung cho toàn bộ các đối tượng thuộc Class đó.

Khi một thành phần được khai báo static, chỉ có một thành phần dùng chung được quản lý trong phạm vi Class. Việc tạo thêm Object Instance không tạo ra một bản sao static mới.

Vì static thuộc về Class nên nó có thể được truy xuất mà không cần tạo Object Instance.

Cách truy xuất thành phần static là thông qua tên Class và tên thành phần static.

Các thành phần instance thì thuộc về từng Object Instance. Mỗi đối tượng có thể có giá trị riêng đối với các thành phần này.

Sự khác nhau giữa static và instance nằm ở phạm vi sở hữu thành phần.

Static thuộc về Class và được dùng chung.

Instance thuộc về Object và mỗi Object có một phiên bản riêng.

Vì static không thuộc về Object Instance nên việc truy xuất thông qua một đối tượng không phản ánh đúng bản chất của thành phần static.

Thành phần static được sử dụng khi dữ liệu hoặc phương thức cần được chia sẻ chung cho toàn bộ Class và không phụ thuộc vào trạng thái của một Object Instance cụ thể.
