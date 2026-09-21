FROM python:3.12-bookworm
RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential cmake git swig && rm -rf /var/lib/apt/lists/*
RUN git init /opt/libiec61850 && cd /opt/libiec61850 && \
    git remote add origin https://github.com/mz-automation/libiec61850.git && \
    git fetch --depth 1 origin 96d69e9c9f7ee270ace1838bb2a5a202a67c78d2 && \
    git checkout FETCH_HEAD
RUN sed -i "1i%ignore GoosePublisher_createRemote;\n%ignore GooseReceiver_createRemote;" /opt/libiec61850/pyiec61850/iec61850.i
RUN cmake -S /opt/libiec61850 -B /opt/libiec61850/build \
    -DBUILD_PYTHON_BINDINGS=ON -DBUILD_EXAMPLES=ON && \
    cmake --build /opt/libiec61850/build --parallel 4 && \
    cmake --install /opt/libiec61850/build && ldconfig
WORKDIR /exercise
