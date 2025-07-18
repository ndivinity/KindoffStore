create table "products" (
    "product_id" integer not null unique,
    "product_name" text not null,
    "product_description" text not null,
    "product_weight" integer not null,
    "product_weight_unit" text not null,
    "product_existence" integer not null,

    primary key("product_id" autoincrement)
)