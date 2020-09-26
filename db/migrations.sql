CREATE TABLE "user" (
  "id" SERIAL PRIMARY KEY,
  "login" varchar,
  "password" varchar,
  "email" varchar
);

CREATE TABLE "desk" (
  "id" SERIAL PRIMARY KEY,
  "title" varchar,
  "description" text
);

CREATE TABLE "desk_column" (
  "id" SERIAL PRIMARY KEY,
  "id_desk" int,
  "label" varchar,
  "order" int
);

CREATE TABLE "user_x_desk" (
  "id_user" int,
  "id_desk" int
);

CREATE TABLE "card" (
  "id" SERIAL PRIMARY KEY,
  "id_column" int,
  "content" text,
  "order" int
);

ALTER TABLE "desk_column" ADD FOREIGN KEY ("id_desk") REFERENCES "desk" ("id");

ALTER TABLE "user_x_desk" ADD FOREIGN KEY ("id_user") REFERENCES "user" ("id");

ALTER TABLE "user_x_desk" ADD FOREIGN KEY ("id_desk") REFERENCES "desk" ("id");

ALTER TABLE "card" ADD FOREIGN KEY ("id_column") REFERENCES "desk_column" ("id");
