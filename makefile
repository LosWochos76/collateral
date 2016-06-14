ifeq ($(OS),Windows_NT)
    TARGET := AlgorithmsInCpp.exe
else
    UNAME_S := $(shell uname -s)
    ifeq ($(UNAME_S),Linux)
        TARGET := AlgorithmsInCpp
    endif
    ifeq ($(UNAME_S),Darwin)
        TARGET := AlgorithmsInCpp
    endif
endif

SRCS := ArrayList.cpp BFS.cpp Graph.cpp GraphAL.cpp GraphAM.cpp Map.cpp String.cpp main.cpp
RELEASE_SRCS := $(addprefix src/,$(SRCS))
RELEASE_OBJS := $(addprefix release/,$(patsubst %.cpp,%.o,$(SRCS)))
DEBUG_OBJS := $(addprefix debug/,$(patsubst %.cpp,%.o,$(SRCS)))
TEST_SRCS := ArrayList.cpp BFS.cpp Graph.cpp GraphAL.cpp GraphAM.cpp Map.cpp String.cpp \
    ArrayListTest.cpp GraphALTest.cpp MapTest.cpp StringTest.cpp UnitTest.cpp tests.cpp
TEST_OBJS := $(addprefix test/,$(patsubst %.cpp,%.o,$(TEST_SRCS)))
TEST_SRCS := $(addprefix src/,$(TEST_SRCS))
CC := g++ -std=c++11

all: release

release: $(RELEASE_OBJS)
	$(CC) $(RELEASE_OBJS) -o release/$(TARGET)

release/%.o: src/%.cpp | release_prepare
	$(CC) -c $< -o $@

release_prepare:
	-mkdir -p release
	
debug: $(DEBUG_OBJS)
	$(CC) $(DEBUG_OBJS) -o debug/$(TARGET)

debug_prepare:
	mkdir -p debug

debug/%.o: src/%.cpp | debug_prepare
	$(CC) -c $< -o $@

test: test_compile | run_tests

test_compile: $(TEST_OBJS)
	$(CC) $(TEST_OBJS) -o test/$(TARGET)

test_prepare: 
	mkdir -p test
	
run_tests:
	test/$(TARGET)

test/%.o: src/%.cpp | test_prepare
	$(CC) -c $< -o $@

clean:
	rm -rf release debug test
	
.PHONY: release_prepare debug_prepare test_compile test_prepare run_tests clean