#ifndef STRING_H
#define	STRING_H

#include <iostream>

class String {
private:
    const char *text;
    int len;
    bool isAtPos(const char* other, int pos, int len) const;
    
public:
    String(const char *text);
    ~String();
    int length() const;
    String toUpper();
    String toLower();
    int find(const String &other) const;
    String concat(const String &other) const;
    String operator+(const String &other) const;
    friend std::ostream& operator<<(std::ostream &out, const String &s);
    bool equals(const String &other);
    bool operator==(const String &other);
};

#endif