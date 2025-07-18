insert into "products" (
    product_name,
    product_description,
    product_weight,
    product_weight_unit,
    product_existence
) values (
    "test name", "test description", "test weight", "grams", cast(abs(random()) / 184467440737095517 as integer) + 1
), (
    "test name", "test description", "test weight", "kilograms", cast(abs(random()) / 184467440737095517 as integer) + 1
), (
    "test name", "test description", "test weight", "grams", cast(abs(random()) / 184467440737095517 as integer) + 1
), (
    "test name", "test description", "test weight", "litres", cast(abs(random()) / 184467440737095517 as integer) + 1
), (
    "test name", "test description", "test weight", "mililitres", cast(abs(random()) / 184467440737095517 as integer) + 1
), (
    "test name", "test description", "test weight", "litres", cast(abs(random()) / 184467440737095517 as integer) + 1
)