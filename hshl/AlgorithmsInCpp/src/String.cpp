#include "String.h"

String::String(const char* text) {
    len = 0;
    while (text[len] != '\0')
        len++;
    
    char* content = new char[len+1];
    for (int i=0; i<len; i++)
        content[i] = text[i];
    
    content[len] = '\0';
    this->text = content;
}

String::~String() {
    delete[] text;
}

int String::length() const {
    return len;
}

bool String::equals(const String& other) {
    int l1 = this->length();
    int l2 = other.length();
    
    if (l1 != l2)
        return false;
    
    for (int i=0; i<l1; i++)
        if (text[i] != other.text[i])
            return false;
        
    return true;
}

bool String::operator==(const String& other) {
    return equals(other);
}

String String::toLower() {
    int l = length();
    char *result = new char[l];
    for (int i=0; i<l; i++)
        if ((text[i] >= 65) && (text[i] <= 90))
            result[i] = text[i] + 32;
        else
            result[i] = text[i];

    return String(result);
}

String String::toUpper() {
    int l = length();
    char *result = new char[l];
    for (int i=0; i<l; i++)
        if ((text[i] >= 97) && (text[i] <= 122))
            result[i] = text[i] - 32;
        else
            result[i] = text[i];

    return String(result);
}

String String::concat(const String& other) const {
    int l1 = this->length();
    int l2 = other.length();
    char *result = new char[l1 + l2 + 1];
    
    for (int i=0; i<l1; i++)
        result[i] = text[i];
    
    for (int i=0; i<l2; i++)
        result[l1+i] = other.text[i];
    
    result[l1+l2] = '\0';
    return String(result);
}

String String::operator+(const String& other) const {
    return concat(other);
}

int String::find(const String& other) const {
    for (int i=0; i<len-other.len+1; i++)
        if (isAtPos(other.text, i, other.len))
            return i;
    
    return -1;
}

bool String::isAtPos(const char* other, int pos, int len) const {
    for (int i=0; i<len; i++)
        if (text[i+pos] != other[i])
            return false;
    
    return true;
}

std::ostream& operator<<(std::ostream &os, const String &s) {
    int pos=0;
    while (s.text[pos] != '\0') {
        os << s.text[pos];
        pos++;
    }
    os.flush();
    return os;
}
