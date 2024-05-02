from imresamu/postgis-arm64

RUN apt update  
RUN apt install -y curl ca-certificates curl gpg
RUN curl -fsS https://www.pgadmin.org/static/packages_pgadmin_org.pub | gpg --dearmor -o /usr/share/keyrings/packages-pgadmin-org.gpg
RUN sh -c 'echo "deb [signed-by=/usr/share/keyrings/packages-pgadmin-org.gpg] https://ftp.postgresql.org/pub/pgadmin/pgadmin4/apt/jammy pgadmin4 main" > /etc/apt/sources.list.d/pgadmin4.list'

RUN apt update 
RUN apt install libgmp3-dev libpq-dev python3
RUN mkdir -p /var/lib/pgadmin4/sessions
RUN mkdir /var/lib/pgadmin4/storage
RUN mkdir /var/log/pgadmin4
RUN python3 -m pip install -U pip
RUN wget https://ftp.postgresql.org/pub/pgadmin/pgadmin4/v6.10/pip/pgadmin4-6.10-py3-none-any.whl
RUN python -m pip install wheel
RUN python -m pip install pgadmin4-6.10-py3-none-any.whl
RUN python -m pip install gunicorn


EXPOSE 5432
EXPOSE 80

ENTRYPOINT ["tail"]
CMD ["-f","/dev/null"]