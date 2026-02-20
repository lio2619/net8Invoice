START TRANSACTION;
ALTER TABLE "Product" ADD "BoxCost" numeric;
COMMENT ON COLUMN "Product"."BoxCost" IS 'Ωc¡ ¶®•ª';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260220151951_AddProductBoxCost', '9.0.9');

COMMIT;