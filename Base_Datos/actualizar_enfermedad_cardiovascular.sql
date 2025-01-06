-- FUNCTION: admece.actualizar_enfermedad_cardiovascular(integer, character varying, character varying, boolean, date)

-- DROP FUNCTION IF EXISTS admece.actualizar_enfermedad_cardiovascular(integer, character varying, character varying, boolean, date);

CREATE OR REPLACE FUNCTION admece.actualizar_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer,
	p_nombre character varying,
	p_descripcion character varying,
	p_estado boolean,
	p_fecha_actualizacion date)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, estado boolean, fecha_actualizacio date) 
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
        estado = COALESCE(p_estado, t.estado),
		fecha_actualizacion = COALESCE(p_fecha_actualizacion, t.fecha_actualizacion)
    WHERE t.id_enf_cardiovascular = p_id_enf_cardiovascular
    RETURNING 
        t.id_enf_cardiovascular, 
        t.nombre, 
        t.descripcion, 
        t.estado,
		t.fecha_actualizacion;
END;
$BODY$;

ALTER FUNCTION admece.actualizar_enfermedad_cardiovascular(integer, character varying, character varying, boolean, date)
    OWNER TO postgres;
