FROM python:3.12

COPY ./requirements.txt /app/requirements.txt
WORKDIR /app
RUN pip install --no-cache-dir --upgrade -r requirements.txt

COPY . /app
CMD [ "python3", "-m" , "flask", "run", "--host=0.0.0.0"]