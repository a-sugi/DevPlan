update �g���b�N_���
set
  �g���b�N_���.�\��\�J�n�� = trunc(SYSDATE) 
where
  �g���b�N_���.flag_�\��� = 1;

update �g���b�N_���
set
  �g���b�N_���.�\��\�J�n�� = trunc(to_date('9998-12-31'))
where
  �g���b�N_���.flag_�\��� = 0;

commit;
