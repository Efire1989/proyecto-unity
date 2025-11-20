/**
 * C++ String vs String_View Performance Comparison Examples
 * C++ String 与 String_View 性能对比示例
 * 
 * This file demonstrates the differences between std::string and std::string_view
 * 本文件演示 std::string 和 std::string_view 之间的差异
 * 
 * Compile with C++17 or later:
 * 使用 C++17 或更高版本编译：
 * g++ -std=c++17 -O2 string_vs_string_view_examples.cpp -o string_comparison
 */

#include <iostream>
#include <string>
#include <string_view>
#include <chrono>
#include <vector>
#include <iomanip>

// ============================================================================
// Example 1: Basic Usage / 基本使用
// ============================================================================

void example1_basic_usage() {
    std::cout << "\n=== Example 1: Basic Usage / 基本使用 ===\n\n";
    
    // std::string - owns the data / 拥有数据
    std::string str = "Hello, World!";
    std::cout << "std::string: " << str << std::endl;
    std::cout << "Size: " << sizeof(str) << " bytes (object) + " 
              << str.capacity() << " bytes (data)\n";
    
    // std::string_view - does not own the data / 不拥有数据
    std::string_view sv = "Hello, World!";
    std::cout << "std::string_view: " << sv << std::endl;
    std::cout << "Size: " << sizeof(sv) << " bytes (only pointer + length)\n";
    
    // Creating view from string / 从字符串创建视图
    std::string_view sv_from_str = str;
    std::cout << "string_view from string: " << sv_from_str << std::endl;
}

// ============================================================================
// Example 2: Function Parameters Performance / 函数参数性能
// ============================================================================

// Inefficient: Copies the entire string / 低效：复制整个字符串
void process_by_string(std::string str) {
    // String is copied here / 这里复制了字符串
    if (str.length() > 0) {
        // Do something / 做某事
    }
}

// Efficient: No copy, just pointer + size / 高效：无复制，仅指针+大小
void process_by_string_view(std::string_view sv) {
    // No copy! Only 16 bytes passed / 无复制！仅传递16字节
    if (sv.length() > 0) {
        // Do something / 做某事
    }
}

void example2_function_parameters() {
    std::cout << "\n=== Example 2: Function Parameters Performance / 函数参数性能 ===\n\n";
    
    std::string large_string(10000, 'A'); // 10KB string / 10KB字符串
    
    // Benchmark string parameter / 基准测试字符串参数
    auto start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < 100000; ++i) {
        process_by_string(large_string);  // Copies 10KB each time! / 每次复制10KB！
    }
    auto end = std::chrono::high_resolution_clock::now();
    auto duration_string = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);
    
    // Benchmark string_view parameter / 基准测试string_view参数
    start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < 100000; ++i) {
        process_by_string_view(large_string);  // Copies only 16 bytes! / 仅复制16字节！
    }
    end = std::chrono::high_resolution_clock::now();
    auto duration_sv = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);
    
    std::cout << "std::string parameter: " << duration_string.count() << " ms\n";
    std::cout << "std::string_view parameter: " << duration_sv.count() << " ms\n";
    std::cout << "Speedup: " << std::fixed << std::setprecision(2) 
              << (double)duration_string.count() / duration_sv.count() << "x faster\n";
}

// ============================================================================
// Example 3: Substring Operations / 子串操作
// ============================================================================

void example3_substring_operations() {
    std::cout << "\n=== Example 3: Substring Operations / 子串操作 ===\n\n";
    
    std::string data = "The quick brown fox jumps over the lazy dog";
    std::string_view data_sv = data;
    
    // Using string::substr (allocates memory) / 使用string::substr（分配内存）
    auto start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < 100000; ++i) {
        std::string sub1 = data.substr(4, 5);   // Allocates / 分配
        std::string sub2 = data.substr(16, 3);  // Allocates / 分配
        std::string sub3 = data.substr(31, 4);  // Allocates / 分配
    }
    auto end = std::chrono::high_resolution_clock::now();
    auto duration_string = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);
    
    // Using string_view::substr (no allocation) / 使用string_view::substr（无分配）
    start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < 100000; ++i) {
        std::string_view sub1 = data_sv.substr(4, 5);   // No allocation / 无分配
        std::string_view sub2 = data_sv.substr(16, 3);  // No allocation / 无分配
        std::string_view sub3 = data_sv.substr(31, 4);  // No allocation / 无分配
    }
    end = std::chrono::high_resolution_clock::now();
    auto duration_sv = std::chrono::duration_cast<std::chrono::milliseconds>(end - start);
    
    // Display results / 显示结果
    std::cout << "3 substrings with std::string: " << duration_string.count() << " ms\n";
    std::cout << "3 substrings with std::string_view: " << duration_sv.count() << " ms\n";
    std::cout << "Speedup: " << std::fixed << std::setprecision(2) 
              << (double)duration_string.count() / duration_sv.count() << "x faster\n";
    
    // Show the actual substrings / 显示实际的子串
    std::cout << "\nExtracted words / 提取的单词:\n";
    std::cout << "  - " << data_sv.substr(4, 5) << "\n";
    std::cout << "  - " << data_sv.substr(16, 3) << "\n";
    std::cout << "  - " << data_sv.substr(31, 4) << "\n";
}

// ============================================================================
// Example 4: Efficient Tokenization / 高效分词
// ============================================================================

std::vector<std::string_view> tokenize(std::string_view text, char delimiter) {
    std::vector<std::string_view> tokens;
    size_t start = 0;
    size_t end = text.find(delimiter);
    
    while (end != std::string_view::npos) {
        tokens.push_back(text.substr(start, end - start));
        start = end + 1;
        end = text.find(delimiter, start);
    }
    
    tokens.push_back(text.substr(start));
    return tokens;
}

void example4_tokenization() {
    std::cout << "\n=== Example 4: Efficient Tokenization / 高效分词 ===\n\n";
    
    std::string csv_line = "John,Doe,30,Engineer,New York,USA,john.doe@email.com";
    
    // Tokenize using string_view (zero allocations for views) 
    // 使用string_view分词（视图零分配）
    auto tokens = tokenize(csv_line, ',');
    
    std::cout << "CSV fields / CSV字段:\n";
    for (size_t i = 0; i < tokens.size(); ++i) {
        std::cout << "  Field " << i << ": " << tokens[i] << "\n";
    }
    
    std::cout << "\nTotal tokens: " << tokens.size() << "\n";
    std::cout << "Memory allocations for views: 0 (only vector allocations)\n";
    std::cout << "内存分配数量：0（仅向量分配）\n";
}

// ============================================================================
// Example 5: Lifetime Safety Issues / 生命周期安全问题
// ============================================================================

// ❌ DANGEROUS: Returns string_view to local string / 危险：返回局部字符串的string_view
std::string_view dangerous_function() {
    std::string local = "This will be destroyed!";
    return std::string_view(local);  // ❌ BAD! Dangling reference / 悬空引用
}

// ✅ SAFE: Returns string_view to string literal / 安全：返回字符串字面量的string_view
std::string_view safe_function() {
    return "String literals have static lifetime";  // ✅ GOOD
}

// ✅ SAFE: Returns string (owns the data) / 安全：返回string（拥有数据）
std::string safe_function_with_string() {
    std::string local = "This is copied and returned safely";
    return local;  // ✅ GOOD - return value optimization
}

void example5_lifetime_safety() {
    std::cout << "\n=== Example 5: Lifetime Safety / 生命周期安全 ===\n\n";
    
    // ✅ Safe usage / 安全用法
    std::string_view sv1 = safe_function();
    std::cout << "Safe string_view: " << sv1 << "\n";
    
    std::string str = safe_function_with_string();
    std::cout << "Safe string: " << str << "\n";
    
    // String view from local string / 从局部字符串创建string_view
    {
        std::string local_string = "Local data";
        std::string_view sv2 = local_string;
        std::cout << "string_view while source is alive: " << sv2 << "\n";
    }
    // sv2 would be dangling here if we tried to use it!
    // 如果在这里尝试使用sv2，它将是悬空的！
    
    std::cout << "\n⚠️  Warning: Never return string_view to local variables!\n";
    std::cout << "⚠️  警告：永远不要返回局部变量的string_view！\n";
}

// ============================================================================
// Example 6: Comparison Operations / 比较操作
// ============================================================================

void example6_comparison() {
    std::cout << "\n=== Example 6: Comparison Operations / 比较操作 ===\n\n";
    
    std::string str1 = "apple";
    std::string str2 = "banana";
    std::string_view sv1 = "apple";
    std::string_view sv2 = "banana";
    
    // String comparison / 字符串比较
    std::cout << "String comparison / 字符串比较:\n";
    std::cout << "  str1 == \"apple\": " << (str1 == "apple") << "\n";
    std::cout << "  str1 < str2: " << (str1 < str2) << "\n";
    
    // String_view comparison / String_view比较
    std::cout << "\nString_view comparison / String_view比较:\n";
    std::cout << "  sv1 == \"apple\": " << (sv1 == "apple") << "\n";
    std::cout << "  sv1 < sv2: " << (sv1 < sv2) << "\n";
    
    // Cross comparison / 交叉比较
    std::cout << "\nCross comparison / 交叉比较:\n";
    std::cout << "  str1 == sv1: " << (str1 == sv1) << "\n";
    std::cout << "  sv1 == str1: " << (sv1 == str1) << "\n";
    
    std::cout << "\nComparison performance is similar for both types.\n";
    std::cout << "两种类型的比较性能相似。\n";
}

// ============================================================================
// Example 7: Modifying String / 修改字符串
// ============================================================================

void example7_modification() {
    std::cout << "\n=== Example 7: String Modification / 字符串修改 ===\n\n";
    
    // std::string is mutable / std::string是可变的
    std::string str = "Hello";
    std::cout << "Original string: " << str << "\n";
    
    str += ", World!";  // ✅ Can append / 可以追加
    std::cout << "After append: " << str << "\n";
    
    str[0] = 'h';  // ✅ Can modify / 可以修改
    std::cout << "After modification: " << str << "\n";
    
    str.insert(5, " Beautiful");  // ✅ Can insert / 可以插入
    std::cout << "After insert: " << str << "\n";
    
    // std::string_view is immutable / std::string_view是不可变的
    std::string_view sv = str;
    std::cout << "\nString_view: " << sv << "\n";
    std::cout << "string_view is READ-ONLY / string_view是只读的\n";
    std::cout << "Cannot modify, append, or insert!\n";
    std::cout << "不能修改、追加或插入！\n";
    
    // But you can create new views / 但可以创建新视图
    std::string_view sv_part = sv.substr(0, 5);
    std::cout << "Can create subview: " << sv_part << "\n";
}

// ============================================================================
// Example 8: Converting Between Types / 类型转换
// ============================================================================

void example8_conversions() {
    std::cout << "\n=== Example 8: Type Conversions / 类型转换 ===\n\n";
    
    // string to string_view (cheap, implicit) / string到string_view（廉价，隐式）
    std::string str = "Hello, World!";
    std::string_view sv = str;  // ✅ Implicit conversion, no copy / 隐式转换，无复制
    std::cout << "string to string_view: " << sv << " (cheap)\n";
    
    // string_view to string (expensive, explicit) / string_view到string（昂贵，显式）
    std::string_view sv2 = "Another string";
    std::string str2(sv2);  // Creates a copy / 创建副本
    std::cout << "string_view to string: " << str2 << " (expensive - allocates memory)\n";
    
    // C-string to both / C字符串到两者
    const char* cstr = "C-style string";
    std::string str3 = cstr;       // Allocates and copies / 分配并复制
    std::string_view sv3 = cstr;   // Just points to it / 仅指向它
    std::cout << "C-string to string: " << str3 << "\n";
    std::cout << "C-string to string_view: " << sv3 << "\n";
    
    std::cout << "\nConversion summary / 转换总结:\n";
    std::cout << "  string → string_view: Cheap (O(1))\n";
    std::cout << "  string_view → string: Expensive (O(n))\n";
    std::cout << "  const char* → string_view: Cheap (O(1))\n";
    std::cout << "  const char* → string: Expensive (O(n))\n";
}

// ============================================================================
// Example 9: Real-World Use Case - Configuration Parser / 实际应用案例 - 配置解析器
// ============================================================================

struct ConfigValue {
    std::string_view key;
    std::string_view value;
};

std::vector<ConfigValue> parse_config(std::string_view config_text) {
    std::vector<ConfigValue> results;
    
    size_t pos = 0;
    while (pos < config_text.length()) {
        // Find end of line / 查找行尾
        size_t line_end = config_text.find('\n', pos);
        if (line_end == std::string_view::npos) {
            line_end = config_text.length();
        }
        
        std::string_view line = config_text.substr(pos, line_end - pos);
        
        // Find '=' separator / 查找'='分隔符
        size_t eq_pos = line.find('=');
        if (eq_pos != std::string_view::npos) {
            ConfigValue cv;
            cv.key = line.substr(0, eq_pos);
            cv.value = line.substr(eq_pos + 1);
            results.push_back(cv);
        }
        
        pos = line_end + 1;
    }
    
    return results;
}

void example9_real_world_use_case() {
    std::cout << "\n=== Example 9: Real-World Use Case - Config Parser / 配置解析器 ===\n\n";
    
    std::string config = 
        "host=localhost\n"
        "port=8080\n"
        "timeout=30\n"
        "debug=true\n"
        "max_connections=100\n";
    
    auto config_values = parse_config(config);
    
    std::cout << "Parsed configuration / 解析的配置:\n";
    for (const auto& cv : config_values) {
        std::cout << "  " << cv.key << " = " << cv.value << "\n";
    }
    
    std::cout << "\nBenefit / 优势: Zero allocations during parsing!\n";
    std::cout << "解析期间零分配！\n";
}

// ============================================================================
// Main Function / 主函数
// ============================================================================

int main() {
    std::cout << "========================================\n";
    std::cout << "C++ String vs String_View Examples\n";
    std::cout << "C++ String 与 String_View 示例\n";
    std::cout << "========================================\n";
    
    example1_basic_usage();
    example2_function_parameters();
    example3_substring_operations();
    example4_tokenization();
    example5_lifetime_safety();
    example6_comparison();
    example7_modification();
    example8_conversions();
    example9_real_world_use_case();
    
    std::cout << "\n========================================\n";
    std::cout << "Summary / 总结:\n";
    std::cout << "========================================\n";
    std::cout << "✅ Use string_view for read-only access (FASTER)\n";
    std::cout << "✅ Use string when you need to own/modify data\n";
    std::cout << "✅ string_view avoids allocations and copies\n";
    std::cout << "⚠️  Always ensure source data outlives string_view\n\n";
    
    std::cout << "✅ 只读访问使用 string_view（更快）\n";
    std::cout << "✅ 需要拥有/修改数据时使用 string\n";
    std::cout << "✅ string_view 避免分配和复制\n";
    std::cout << "⚠️  始终确保源数据的生命周期长于 string_view\n";
    std::cout << "========================================\n";
    
    return 0;
}

/**
 * Compilation and Execution / 编译和执行:
 * 
 * g++ -std=c++17 -O2 string_vs_string_view_examples.cpp -o string_comparison
 * ./string_comparison
 * 
 * Expected Output / 预期输出:
 * - Demonstrates all key differences / 演示所有关键差异
 * - Shows performance comparisons / 显示性能比较
 * - Highlights safety considerations / 强调安全注意事项
 * - Provides practical examples / 提供实际示例
 */
