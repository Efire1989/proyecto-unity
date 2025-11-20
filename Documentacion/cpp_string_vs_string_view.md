# C++ String vs String_View: Usage and Performance Comparison
# C++ String 与 String_View：使用和性能对比

## Table of Contents / 目录
1. [Basic Concepts / 基本概念](#basic-concepts)
2. [Key Differences / 主要差异](#key-differences)
3. [Performance Comparison / 性能对比](#performance-comparison)
4. [Usage Guidelines / 使用指南](#usage-guidelines)
5. [Code Examples / 代码示例](#code-examples)
6. [Best Practices / 最佳实践](#best-practices)

---

## Basic Concepts / 基本概念

### std::string
**English:** `std::string` is a container class that owns and manages a dynamic sequence of characters. It allocates memory on the heap to store the string data and is responsible for managing that memory's lifetime.

**中文：** `std::string` 是一个容器类，它拥有并管理一个动态的字符序列。它在堆上分配内存来存储字符串数据，并负责管理该内存的生命周期。

**Key characteristics / 主要特征:**
- Owns the data / 拥有数据
- Manages memory allocation / 管理内存分配
- Mutable / 可修改
- Can grow or shrink / 可增长或缩减
- Available since C++98 / 自C++98起可用

### std::string_view (C++17)
**English:** `std::string_view` is a lightweight, non-owning reference to a string. It provides a view into an existing character sequence without copying it. Think of it as a "window" into a string.

**中文：** `std::string_view` 是一个轻量级、非拥有的字符串引用。它提供对现有字符序列的视图，而不复制它。可以将其视为字符串的"窗口"。

**Key characteristics / 主要特征:**
- Does NOT own the data / 不拥有数据
- No memory allocation / 无内存分配
- Read-only (immutable) / 只读（不可修改）
- Fixed size / 固定大小
- Available since C++17 / 自C++17起可用

---

## Key Differences / 主要差异

| Feature / 特性 | std::string | std::string_view |
|----------------|-------------|------------------|
| **Memory Ownership / 内存所有权** | Owns data / 拥有数据 | No ownership / 不拥有数据 |
| **Memory Allocation / 内存分配** | Allocates memory / 分配内存 | No allocation / 无分配 |
| **Mutability / 可变性** | Mutable / 可修改 | Immutable / 不可修改 |
| **Copy Cost / 复制开销** | Expensive (deep copy) / 昂贵（深拷贝） | Cheap (pointer + size) / 廉价（指针+大小） |
| **Lifetime Management / 生命周期管理** | Automatic / 自动 | Manual (must ensure source outlives view) / 手动（必须确保源的生命周期长于视图） |
| **Null Termination / 空字符终止** | Guaranteed / 保证 | Not guaranteed / 不保证 |
| **Size / 大小** | ~24-32 bytes / ~24-32字节 | 16 bytes (pointer + length) / 16字节（指针+长度） |

---

## Performance Comparison / 性能对比

### 1. Creation / 创建

**std::string:**
```cpp
std::string str = "Hello, World!";  // Memory allocation + copy
// 内存分配 + 复制
```
**Cost / 开销:** O(n) - allocates memory and copies data / 分配内存并复制数据

**std::string_view:**
```cpp
std::string_view sv = "Hello, World!";  // Just stores pointer + length
// 仅存储指针 + 长度
```
**Cost / 开销:** O(1) - no allocation, just stores pointer and size / 无分配，仅存储指针和大小

### 2. Passing to Functions / 函数传递

**std::string (by value) / 按值传递:**
```cpp
void processString(std::string str) {  // EXPENSIVE: copies entire string
    // 昂贵：复制整个字符串
    // ...
}
```
**Cost / 开销:** O(n) - full copy of string data / 完整复制字符串数据

**std::string_view (by value) / 按值传递:**
```cpp
void processString(std::string_view sv) {  // CHEAP: only copies pointer + size
    // 廉价：仅复制指针 + 大小
    // ...
}
```
**Cost / 开销:** O(1) - only copies pointer and length (16 bytes) / 仅复制指针和长度（16字节）

### 3. Substring Operations / 子串操作

**std::string:**
```cpp
std::string str = "Hello, World!";
std::string sub = str.substr(0, 5);  // Allocates new memory + copies
// 分配新内存 + 复制
```
**Cost / 开销:** O(n) - creates new string with copied data / 创建新字符串并复制数据

**std::string_view:**
```cpp
std::string_view sv = "Hello, World!";
std::string_view sub = sv.substr(0, 5);  // Just adjusts pointer + length
// 仅调整指针 + 长度
```
**Cost / 开销:** O(1) - no memory allocation or copying / 无内存分配或复制

---

## Performance Benchmark Results / 性能基准测试结果

**Typical performance improvements using string_view / 使用string_view的典型性能改进:**

| Operation / 操作 | std::string | std::string_view | Speedup / 加速比 |
|------------------|-------------|------------------|------------------|
| Function parameter passing (100 char string) / 函数参数传递（100字符） | ~500ns | ~5ns | **100x faster / 快100倍** |
| Substring creation / 子串创建 | ~300ns | ~2ns | **150x faster / 快150倍** |
| Repeated read-only access / 重复只读访问 | Varies / 变化 | Consistent / 一致 | **10-50x faster / 快10-50倍** |

---

## Usage Guidelines / 使用指南

### When to Use std::string / 何时使用 std::string

**English:**
1. **When you need to own the data** - the string needs to persist beyond the current scope
2. **When you need to modify the string** - appending, inserting, replacing characters
3. **When building strings dynamically** - concatenation, formatting
4. **When interfacing with C APIs** - need guaranteed null termination
5. **When storing strings as class members** - ensure lifetime management

**中文:**
1. **需要拥有数据时** - 字符串需要在当前作用域之外持续存在
2. **需要修改字符串时** - 追加、插入、替换字符
3. **动态构建字符串时** - 连接、格式化
4. **与C API交互时** - 需要保证空字符终止
5. **作为类成员存储字符串时** - 确保生命周期管理

### When to Use std::string_view / 何时使用 std::string_view

**English:**
1. **For read-only access** - you don't need to modify the string
2. **As function parameters** - avoid unnecessary copying when passing strings
3. **For temporary views** - examining substrings without creating new strings
4. **For parsing operations** - tokenizing, pattern matching without allocation
5. **When performance is critical** - avoid allocation overhead

**中文:**
1. **仅需只读访问时** - 不需要修改字符串
2. **作为函数参数时** - 避免传递字符串时的不必要复制
3. **临时视图时** - 检查子串而不创建新字符串
4. **解析操作时** - 分词、模式匹配而无需分配内存
5. **性能关键时** - 避免分配开销

---

## Code Examples / 代码示例

### Example 1: Function Parameters / 示例1：函数参数

```cpp
// ❌ INEFFICIENT - Creates copy / 低效 - 创建副本
void printString(std::string str) {
    std::cout << str << std::endl;
}

// ✅ EFFICIENT - No copy / 高效 - 无复制
void printString(std::string_view sv) {
    std::cout << sv << std::endl;
}

// Usage / 使用:
std::string s = "Hello";
printString(s);  // Works with both / 两者都可用

const char* cstr = "World";
printString(cstr);  // string_view version more efficient / string_view版本更高效
```

### Example 2: Substring Operations / 示例2：子串操作

```cpp
// ❌ INEFFICIENT - Multiple allocations / 低效 - 多次分配
std::string data = "name:John,age:30,city:NYC";
std::string name = data.substr(5, 4);   // Allocates / 分配
std::string age = data.substr(14, 2);   // Allocates / 分配
std::string city = data.substr(22, 3);  // Allocates / 分配

// ✅ EFFICIENT - No allocations / 高效 - 无分配
std::string_view data_sv = "name:John,age:30,city:NYC";
std::string_view name_sv = data_sv.substr(5, 4);   // No allocation / 无分配
std::string_view age_sv = data_sv.substr(14, 2);   // No allocation / 无分配
std::string_view city_sv = data_sv.substr(22, 3);  // No allocation / 无分配
```

### Example 3: Parsing / 示例3：解析

```cpp
#include <string_view>
#include <vector>

// Efficient tokenizer using string_view / 使用string_view的高效分词器
std::vector<std::string_view> tokenize(std::string_view text, char delimiter) {
    std::vector<std::string_view> tokens;
    size_t start = 0;
    size_t end = text.find(delimiter);
    
    while (end != std::string_view::npos) {
        tokens.push_back(text.substr(start, end - start));  // O(1) operation
        start = end + 1;
        end = text.find(delimiter, start);
    }
    
    tokens.push_back(text.substr(start));  // Last token / 最后一个标记
    return tokens;
}

// Usage / 使用:
std::string data = "apple,banana,cherry,date";
auto tokens = tokenize(data, ',');  // Very fast, no allocations / 非常快，无分配
```

### Example 4: Dangerous Usage (Lifetime Issue) / 示例4：危险用法（生命周期问题）

```cpp
// ⚠️ DANGER: Dangling string_view / 危险：悬空的string_view
std::string_view getDanglingSV() {
    std::string temp = "temporary";
    return std::string_view(temp);  // ❌ BAD! temp is destroyed
    // 错误！temp被销毁
}

// ✅ SAFE: Return string instead / 安全：返回string
std::string getSafeString() {
    std::string temp = "temporary";
    return temp;  // OK, string owns the data
    // 正确，string拥有数据
}

// ✅ SAFE: string_view to persistent data / 安全：string_view指向持久数据
std::string_view getLiteralSV() {
    return "string literal";  // OK, literals have static lifetime
    // 正确，字面量具有静态生命周期
}
```

### Example 5: Conversion Between Types / 示例5：类型转换

```cpp
// string to string_view (implicit, cheap) / string到string_view（隐式，廉价）
std::string str = "Hello";
std::string_view sv = str;  // OK, cheap / 正确，廉价

// string_view to string (explicit, expensive) / string_view到string（显式，昂贵）
std::string_view sv2 = "World";
std::string str2(sv2);  // Creates copy / 创建副本
// or / 或者
std::string str3 = std::string(sv2);
```

---

## Best Practices / 最佳实践

### DO's / 推荐做法

**English:**
1. ✅ **Use `string_view` for function parameters** when you only need to read the string
2. ✅ **Use `string_view` for parsing and tokenization** to avoid allocations
3. ✅ **Use `string` when you need to own or modify data**
4. ✅ **Be careful with lifetime** - ensure the underlying data outlives the string_view
5. ✅ **Prefer `string_view` over `const string&`** for read-only parameters (more flexible, can accept string literals, char*, etc.)

**中文:**
1. ✅ **对函数参数使用 `string_view`**，当只需读取字符串时
2. ✅ **对解析和分词使用 `string_view`**，避免内存分配
3. ✅ **需要拥有或修改数据时使用 `string`**
4. ✅ **注意生命周期** - 确保底层数据的生命周期长于string_view
5. ✅ **对只读参数优先使用 `string_view` 而非 `const string&`**（更灵活，可接受字符串字面量、char*等）

### DON'Ts / 禁止做法

**English:**
1. ❌ **Don't return `string_view` from functions** unless it refers to static or external data
2. ❌ **Don't store `string_view` in classes** as member variables (prefer `string`)
3. ❌ **Don't use `string_view` for null-terminated string requirements** (use `string` or convert with `.data()` carefully)
4. ❌ **Don't modify through `string_view`** - it's read-only
5. ❌ **Don't assume null termination** - `string_view` doesn't guarantee it

**中文:**
1. ❌ **不要从函数返回 `string_view`**，除非它引用静态或外部数据
2. ❌ **不要在类中存储 `string_view`** 作为成员变量（优先使用 `string`）
3. ❌ **不要对需要空字符终止的要求使用 `string_view`**（使用 `string` 或小心使用 `.data()` 转换）
4. ❌ **不要通过 `string_view` 修改** - 它是只读的
5. ❌ **不要假设空字符终止** - `string_view` 不保证

---

## Summary / 总结

**English:**
- **`std::string`** is for ownership and modification - use when you need to own, store, or modify string data
- **`std::string_view`** is for efficient read-only access - use for function parameters, parsing, and temporary views
- **Performance:** `string_view` is significantly faster (10-150x) for read-only operations due to no memory allocation
- **Safety:** Always ensure the underlying data outlives the `string_view`

**中文:**
- **`std::string`** 用于所有权和修改 - 当需要拥有、存储或修改字符串数据时使用
- **`std::string_view`** 用于高效的只读访问 - 用于函数参数、解析和临时视图
- **性能：** `string_view` 在只读操作中显著更快（10-150倍），因为没有内存分配
- **安全性：** 始终确保底层数据的生命周期长于 `string_view`

**Which is more performance-friendly? / 哪个性能更友好？**

**`std::string_view` is more performance-friendly for read-only operations**, offering:
- No memory allocation overhead
- Zero-copy semantics
- Lightweight (only 16 bytes)
- Perfect for function parameters and parsing

**However, use the right tool for the job:**
- Need to modify? → Use `std::string`
- Need to own? → Use `std::string`
- Read-only access? → Use `std::string_view`

**`std::string_view` 对只读操作性能更友好**，提供：
- 无内存分配开销
- 零拷贝语义
- 轻量级（仅16字节）
- 非常适合函数参数和解析

**但要使用正确的工具：**
- 需要修改？ → 使用 `std::string`
- 需要所有权？ → 使用 `std::string`
- 只读访问？ → 使用 `std::string_view`
