CREATE DATABASE "Pruebas_sye"
with
    template = template0
    encoding = 'UTF8'
    lc_collate = 'es_MX.UTF-8'
    lc_ctype = 'es_MX.UTF-8'
    connection limit = -1
    is_template = False;


CREATE SCHEMA IF NOT EXISTS admece

CREATE TABLE IF NOT EXISTS admece.tc_enfermedad_cardiovascular
(
	id_enf_cardiovascular SERIAL,
	nombre VARCHAR(255) NOT NULL,
	descripcion VARCHAR(255)NOT NULL,
	fecha_registro DATE NOT NULL,
	fecha_inicio DATE NOT NULL,
	estado BOOLEAN NOT NULL DEFAULT TRUE,
	fecha_actualizacion DATE NOT NULL,
	PRIMARY KEY(id_enf_cardiovascular)
);

CREATE TABLE IF NOT EXISTS admece.tc_enfermedad_cronica
(
	id_enf_cronica SERIAL,
	nombre VARCHAR(255) NOT NULL,
	descripcion VARCHAR(255)NOT NULL,
	fecha_registro DATE NOT NULL,
	fecha_inicio DATE NOT NULL,
	estado BOOLEAN NOT NULL DEFAULT TRUE,
	fecha_actualizacion DATE NOT NULL,
	PRIMARY KEY(id_enf_cronica)
);



