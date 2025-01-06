-- FUNCTION: admece.actualizar_enfermedad(integer, character varying, character varying, boolean, date)

-- DROP FUNCTION IF EXISTS admece.actualizar_enfermedad(integer, character varying, character varying, boolean, date);

CREATE OR REPLACE FUNCTION admece.actualizar_enfermedad(
	p_id_enf_cronica integer,
	p_nombre character varying,
	p_descripcion character varying,
	p_estado boolean,
	p_fecha_actualizacion date)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, estado boolean, fecha_actualizacion date) 
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
        estado = COALESCE(p_estado, t.estado),
        fecha_actualizacion = COALESCE(p_fecha_actualizacion, t.fecha_actualizacion)  -- Corregido aquí
    WHERE t.id_enf_cronica = p_id_enf_cronica
    RETURNING 
        t.id_enf_cronica, 
        t.nombre, 
        t.descripcion, 
        t.estado,
        t.fecha_actualizacion;
END;
$BODY$;

ALTER FUNCTION admece.actualizar_enfermedad(integer, character varying, character varying, boolean, date)
    OWNER TO postgres;
