----------Funcion de engfermedades cronicas-----------------------------
CREATE OR REPLACE FUNCTION admece.obtener_todos_enfermedades(
	)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cronica, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cronica" t
    WHERE t.estado = TRUE
    ORDER BY t.id_enf_cronica ASC;
END;
$BODY$;

ALTER FUNCTION admece.obtener_todos_enfermedades()
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION admece.agregar_enfermedad(
	p_nombre character varying,
	p_descripcion character varying,
	p_fecha_registro date,
	p_fecha_inicio date,
	p_estado boolean,
	p_fecha_actualizacion date)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
RETURN QUERY 
INSERT INTO admece."tc_enfermedad_cronica" AS t
(nombre,descripcion,fecha_registro,fecha_inicio,estado,fecha_actualizacion)
VALUES 
(p_nombre,p_descripcion,p_fecha_registro,p_fecha_inicio,p_estado,p_fecha_actualizacion)
RETURNING
t.id_enf_cronica, t.nombre, t.descripcion,t.fecha_registro,t.fecha_inicio, t.estado, t.fecha_actualizacion;
END 
$BODY$;
ALTER FUNCTION admece.agregar_enfermedad(character varying, character varying, date, date, boolean, date)
    OWNER TO postgres;


CREATE OR REPLACE FUNCTION admece.actualizar_enfermedad(
	p_id_enf_cronica integer,
	p_nombre character varying,
	p_descripcion character varying,
	p_estado boolean)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    UPDATE admece."tc_enfermedad_cronica" AS t
    SET 
        nombre = COALESCE(p_nombre, t.nombre),
        descripcion = COALESCE(p_descripcion, t.descripcion),
        estado = COALESCE(p_estado, t.estado)
    WHERE t.id_enf_cronica = p_id_enf_cronica
    RETURNING 
        t.id_enf_cronica, 
        t.nombre, 
        t.descripcion, 
        t.estado;
END;
$BODY$;

ALTER FUNCTION admece.actualizar_enfermedad(integer, character varying, character varying, boolean)
    OWNER TO postgres;



	CREATE OR REPLACE FUNCTION admece.borrar_enfermedad(
	p_id_enf_cronica integer)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    -- Actualizamos el estado de la enfermedad crónica y retornamos las tuplas afectadas
    RETURN QUERY
    UPDATE admece."tc_enfermedad_cronica" AS ec
    SET 
        estado = FALSE,
        fecha_actualizacion = CURRENT_DATE
    WHERE ec.id_enf_cronica = p_id_enf_cronica
    RETURNING 
        ec.id_enf_cronica, 
        ec.nombre, 
        ec.descripcion, 
        ec.fecha_registro, 
        ec.fecha_inicio, 
        ec.estado, 
        ec.fecha_actualizacion;
END;
$BODY$;

ALTER FUNCTION admece.borrar_enfermedad(integer)
    OWNER TO postgres;

	--------

CREATE OR REPLACE FUNCTION admece.obtener_id_enfermedad(
	p_id_enf_cronica integer)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cronica, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cronica" t
    WHERE t.id_enf_cronica = p_id_enf_cronica;
END;
$BODY$;

ALTER FUNCTION admece.obtener_id_enfermedad(integer)
    OWNER TO postgres;

-------------Enfermedades cardiovascular------------------------------
CREATE OR REPLACE FUNCTION admece.obtener_enfermedad_cardiovascular(
	)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cardiovascular, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cardiovascular" t
    WHERE t.estado = TRUE
    ORDER BY t.id_enf_cardiovascular ASC;
END;
$BODY$;

ALTER FUNCTION admece.obtener_enfermedad_cardiovascular()
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION admece.agregar_enfermedad_cardiovascular(
	p_nombre character varying,
	p_descripcion character varying,
	p_fecha_registro date,
	p_fecha_inicio date,
	p_estado boolean,
	p_fecha_actualizacion date)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
RETURN QUERY 
INSERT INTO admece."tc_enfermedad_cardiovascular" AS t
(nombre,descripcion,fecha_registro,fecha_inicio,estado,fecha_actualizacion)
VALUES 
(p_nombre,p_descripcion,p_fecha_registro,p_fecha_inicio,p_estado,p_fecha_actualizacion)
RETURNING
t.id_enf_cardiovascular, t.nombre, t.descripcion,t.fecha_registro,t.fecha_inicio, t.estado, t.fecha_actualizacion;
END 
$BODY$;
ALTER FUNCTION admece.agregar_enfermedad(character varying, character varying, date, date, boolean, date)
    OWNER TO postgres;


CREATE OR REPLACE FUNCTION admece.actualizar_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer,
	p_nombre character varying,
	p_descripcion character varying,
	p_estado boolean)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    UPDATE admece."tc_enfermedad_cardiovascular" AS t
    SET 
        nombre = COALESCE(p_nombre, t.nombre),
        descripcion = COALESCE(p_descripcion, t.descripcion),
        estado = COALESCE(p_estado, t.estado)
    WHERE t.id_enf_cardiovascular = p_id_enf_cardiovascular
    RETURNING 
        t.id_enf_cardiovascular, 
        t.nombre, 
        t.descripcion, 
        t.estado;
END;
$BODY$;

ALTER FUNCTION admece.actualizar_enfermedad_cardiovascular(integer, character varying, character varying, boolean)
    OWNER TO postgres;


---------Borrar enfermedad----------------

	CREATE OR REPLACE FUNCTION admece.borrar_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    -- Actualizamos el estado de la enfermedad crónica y retornamos las tuplas afectadas
    RETURN QUERY
    UPDATE admece."tc_enfermedad_cardiovascular" AS ec
    SET 
        estado = FALSE,
        fecha_actualizacion = CURRENT_DATE
    WHERE ec.id_enf_cardiovascular = p_id_enf_cardiovascular
    RETURNING 
        ec.id_enf_cardiovascular, 
        ec.nombre, 
        ec.descripcion, 
        ec.fecha_registro, 
        ec.fecha_inicio, 
        ec.estado, 
        ec.fecha_actualizacion;
END;
$BODY$;

ALTER FUNCTION admece.borrar_enfermedad_cardiovascular(integer)
    OWNER TO postgres;

CREATE OR REPLACE FUNCTION admece.obtener_id_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cardiovascular, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cardiovascular" t
    WHERE t.id_enf_cardiovascular = p_id_enf_cardiovascular;
END;
$BODY$;

ALTER FUNCTION admece.obtener_id_enfermedad_cardiovascular(integer)
    OWNER TO postgres;
