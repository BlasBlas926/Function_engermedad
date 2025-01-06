-- FUNCTION: admece.obtener_id_enfermedad(integer)

-- DROP FUNCTION IF EXISTS admece.obtener_id_enfermedad(integer);

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
