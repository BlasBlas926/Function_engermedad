-- FUNCTION: admece.borrar_enfermedad_cardiovascular(integer)

-- DROP FUNCTION IF EXISTS admece.borrar_enfermedad_cardiovascular(integer);

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
